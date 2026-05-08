using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    // Configuration for retry logic
    private const int MaxRetryAttempts = 5;          // maximum number of attempts
    private const int RetryDelayMilliseconds = 2000; // wait time between attempts

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";
        const string signatureImage = "signature.jpg";

        // Validate input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }
        if (!File.Exists(signatureImage))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImage}");
            return;
        }

        int attempt = 0;
        bool signed = false;

        while (attempt < MaxRetryAttempts && !signed)
        {
            attempt++;
            try
            {
                // Create the facade and bind the source PDF
                using (PdfFileSignature pdfSign = new PdfFileSignature())
                {
                    pdfSign.BindPdf(inputPdf);

                    // Optional: set a visual appearance for the signature
                    pdfSign.SignatureAppearance = signatureImage;

                    // Provide the certificate used for signing
                    pdfSign.SetCertificate(certificatePath, certificatePassword);

                    // Define the rectangle where the visible signature will appear
                    // System.Drawing.Rectangle is required by the Sign method
                    Rectangle rect = new Rectangle(100, 100, 200, 100);

                    // Perform the signing operation
                    pdfSign.Sign(
                        page: 1,                     // page number (1‑based)
                        SigReason: "Approved",       // reason for signing
                        SigContact: "john.doe@example.com", // contact information
                        SigLocation: "New York",     // location
                        visible: true,               // make the signature visible
                        annotRect: rect);            // rectangle on the page

                    // Save the signed PDF
                    pdfSign.Save(outputPdf);
                }

                Console.WriteLine($"PDF signed successfully on attempt {attempt}.");
                signed = true;
            }
            catch (IOException ioEx) when (IsFileLocked(ioEx))
            {
                // The file is locked by another process – wait and retry
                Console.Error.WriteLine($"Attempt {attempt}: PDF file is locked. Retrying in {RetryDelayMilliseconds} ms...");
                Thread.Sleep(RetryDelayMilliseconds);
            }
            catch (Exception ex)
            {
                // Unexpected error – abort retries
                Console.Error.WriteLine($"Signing failed: {ex.Message}");
                break;
            }
        }

        if (!signed)
        {
            Console.Error.WriteLine($"Failed to sign the PDF after {MaxRetryAttempts} attempts.");
        }
    }

    // Helper to detect file‑locking IOExceptions
    private static bool IsFileLocked(IOException exception)
    {
        // HResult 0x20 (32) corresponds to ERROR_SHARING_VIOLATION
        const int ERROR_SHARING_VIOLATION = unchecked((int)0x80070020);
        return exception.HResult == ERROR_SHARING_VIOLATION;
    }
}