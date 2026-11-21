using System;
using System.IO;
using System.Threading;
using System.Drawing; // for System.Drawing.Rectangle
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // PKCS1 class

class Program
{
    static void Main()
    {
        const string inputPdf      = "document.pdf";
        const string outputPdf     = "signed_document.pdf";
        const string certificate   = "certificate.pfx";
        const string certPassword  = "certPass";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificate))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificate}");
            return;
        }

        const int maxRetries   = 5;   // maximum attempts
        const int retryDelayMs = 2000; // wait time between attempts (ms)
        int attempt            = 0;
        bool signedSuccessfully = false;

        while (attempt < maxRetries && !signedSuccessfully)
        {
            attempt++;
            try
            {
                // Use a using block so the PdfFileSignature object is disposed correctly
                using (PdfFileSignature signer = new PdfFileSignature())
                {
                    // Bind the source PDF; may throw IOException if the file is locked
                    signer.BindPdf(inputPdf);

                    // Prepare PKCS1 credentials (certificate + password)
                    PKCS1 pkcs = new PKCS1(certificate, certPassword);

                    // Define the visible signature rectangle (x, y, width, height)
                    // Adjust the values to suit your document layout
                    Rectangle signatureRect = new Rectangle(100, 100, 200, 50);

                    // Sign the PDF. The overload expects:
                    // (int pageNumber, string reason, string contactInfo,
                    //  string location, bool visible, System.Drawing.Rectangle rect, PKCS1 pkcs)
                    signer.Sign(
                        1,                     // page number (1‑based)
                        "I agree",            // reason
                        "contact@example.com", // contact info
                        "New York, USA",      // location
                        true,                  // visible signature flag
                        signatureRect,         // signature rectangle
                        pkcs);                 // PKCS1 object

                    // Save the signed PDF to the desired output path
                    signer.Save(outputPdf);
                }

                signedSuccessfully = true;
                Console.WriteLine($"PDF signed successfully on attempt {attempt}.");
            }
            catch (IOException ioEx)
            {
                // Likely the PDF is locked by another process
                Console.WriteLine($"Attempt {attempt}: PDF is locked ({ioEx.Message}). Retrying in {retryDelayMs} ms...");

                if (attempt >= maxRetries)
                {
                    Console.Error.WriteLine($"Failed to sign after {maxRetries} attempts.");
                    break;
                }

                Thread.Sleep(retryDelayMs);
            }
            catch (Exception ex)
            {
                // Non‑retryable error
                Console.Error.WriteLine($"Signing failed: {ex.Message}");
                break;
            }
        }
    }
}
