using System;
using System.IO;
using System.Threading;
using System.Drawing; // <-- added for Rectangle
using Aspose.Pdf.Facades;

class PdfSigner
{
    /// <summary>
    /// Signs a PDF file with a digital certificate.
    /// Retries the operation if the file is locked by another process.
    /// </summary>
    public static void SignPdfWithRetry(
        string inputPath,
        string outputPath,
        string certificatePath,
        string certificatePassword,
        int pageNumber,
        Rectangle signatureRect, // System.Drawing.Rectangle
        int maxRetries = 3,
        int delayMilliseconds = 1000)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        int attempt = 0;
        while (true)
        {
            attempt++;
            try
            {
                // Use a using block so the facade is disposed (handles file handles) even on failure.
                using (PdfFileSignature pdfSign = new PdfFileSignature())
                {
                    // Bind the source PDF file
                    pdfSign.BindPdf(inputPath);

                    // Set the digital certificate
                    pdfSign.SetCertificate(certificatePath, certificatePassword);

                    // Perform the signature. The overload expects System.Drawing.Rectangle.
                    pdfSign.Sign(pageNumber,
                                 "Document signed",   // Reason
                                 "contact@example.com", // Contact
                                 "Location",          // Location
                                 true,                // Visible
                                 signatureRect);

                    // Save the signed PDF
                    pdfSign.Save(outputPath);
                }

                Console.WriteLine($"PDF signed successfully and saved to '{outputPath}'.");
                break; // Success, exit the retry loop
            }
            catch (IOException) when (attempt < maxRetries)
            {
                // The file may be locked by another process; wait and retry
                Console.Error.WriteLine($"Attempt {attempt} failed: file is locked. Retrying in {delayMilliseconds} ms...");
                Thread.Sleep(delayMilliseconds);
            }
            catch (Exception ex)
            {
                // Any other exception is reported and the operation stops
                Console.Error.WriteLine($"Signing failed: {ex.Message}");
                break;
            }
        }
    }

    // Example usage
    static void Main()
    {
        string inputPdf = "input.pdf";
        string outputPdf = "signed_output.pdf";
        string certFile = "certificate.pfx";
        string certPassword = "password";

        // Define the signature rectangle (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y) in points.
        // System.Drawing.Rectangle constructor uses (x, y, width, height).
        int llx = 100, lly = 100, urx = 300, ury = 200;
        Rectangle rect = new Rectangle(
            llx,
            lly,
            urx - llx,   // width
            ury - lly);  // height

        SignPdfWithRetry(inputPdf, outputPdf, certFile, certPassword, 1, rect);
    }
}
