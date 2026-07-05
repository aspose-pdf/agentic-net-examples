using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath  = "certificate.pfx";
        const string certPass  = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        const int maxRetries = 5;
        const int delayMs    = 2000; // 2 seconds between attempts

        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                // Use PdfFileSignature facade to sign the PDF
                using (PdfFileSignature signer = new PdfFileSignature())
                {
                    // Load the PDF file
                    signer.BindPdf(inputPdf);

                    // Set the certificate for signing
                    signer.SetCertificate(certPath, certPass);

                    // Optional: set a visual appearance for the signature
                    // signer.SignatureAppearance = "signature_image.jpg";

                    // Define the rectangle where the visible signature will appear
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

                    // Perform the signing on page 1
                    signer.Sign(
                        page: 1,
                        SigReason: "Approved",
                        SigContact: "john.doe@example.com",
                        SigLocation: "New York",
                        visible: true,
                        annotRect: rect);

                    // Save the signed PDF
                    signer.Save(outputPdf);
                }

                Console.WriteLine($"PDF signed successfully and saved to '{outputPdf}'.");
                break; // success, exit retry loop
            }
            catch (IOException ioEx) when (IsFileLocked(ioEx))
            {
                // The file is locked by another process
                Console.Error.WriteLine($"Attempt {attempt} failed: file is locked. Retrying in {delayMs} ms...");
                if (attempt == maxRetries)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Signing aborted.");
                    Console.Error.WriteLine(ioEx.Message);
                    return;
                }
                Thread.Sleep(delayMs);
            }
            catch (Exception ex)
            {
                // Any other exception is considered fatal
                Console.Error.WriteLine($"Signing failed: {ex.Message}");
                return;
            }
        }
    }

    // Helper to detect file‑lock errors (Windows specific HRESULT 0x20)
    private static bool IsFileLocked(IOException ex)
    {
        const int ERROR_SHARING_VIOLATION = 0x20;
        return (ex.HResult & 0xFFFF) == ERROR_SHARING_VIOLATION;
    }
}