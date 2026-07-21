using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string outputPath     = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        // Signature parameters
        const int    pageNumber    = 1;
        const bool   visible       = true;
        const string reason        = "Approved";
        const string contact       = "john.doe@example.com";
        const string location      = "New York";

        // Rectangle for the visible signature (System.Drawing.Rectangle)
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Retry configuration
        const int maxAttempts = 5;
        const int delayMilliseconds = 2000;

        int attempt = 0;
        bool signed = false;

        while (attempt < maxAttempts && !signed)
        {
            attempt++;
            try
            {
                // Bind the PDF, configure the signature and save
                using (PdfFileSignature pdfSign = new PdfFileSignature())
                {
                    // Bind the source PDF file
                    pdfSign.BindPdf(inputPath);

                    // Optional: set a visual appearance for the signature
                    // pdfSign.SignatureAppearance = "signature_image.jpg";

                    // Load the certificate used for signing
                    pdfSign.SetCertificate(certificatePath, certificatePassword);

                    // Perform the signing operation
                    pdfSign.Sign(pageNumber, reason, contact, location, visible, rect);

                    // Save the signed PDF to the output file
                    pdfSign.Save(outputPath);
                }

                signed = true;
                Console.WriteLine($"PDF signed successfully on attempt {attempt}.");
            }
            catch (IOException ex) when (IsFileLockedException(ex))
            {
                // The file is locked by another process – wait and retry
                Console.WriteLine($"Attempt {attempt}: PDF file is locked. Retrying in {delayMilliseconds} ms...");
                Thread.Sleep(delayMilliseconds);
            }
            catch (Exception ex)
            {
                // Any other exception is fatal
                Console.Error.WriteLine($"Signing failed: {ex.Message}");
                break;
            }
        }

        if (!signed)
        {
            Console.Error.WriteLine("Failed to sign the PDF after multiple attempts.");
        }
    }

    // Helper to detect file‑lock related IOException
    private static bool IsFileLockedException(IOException ex)
    {
        // HResult 0x20 (32) indicates sharing violation on Windows
        const int ERROR_SHARING_VIOLATION = 0x20;
        return ex.HResult == unchecked((int)0x80070020) || ex.Message.Contains("being used by another process");
    }
}