using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class PdfSigner
{
    // Signs a PDF file with a certificate.
    // Retries the operation if the file is locked by another process.
    public static void SignPdfWithRetry(
        string inputPdfPath,
        string outputPdfPath,
        string certificatePath,
        string certificatePassword,
        int pageNumber,
        System.Drawing.Rectangle signatureRect,
        int maxRetries = 5,
        int delayMilliseconds = 2000)
    {
        int attempt = 0;

        while (true)
        {
            try
            {
                // Create the facade and bind the source PDF.
                using (PdfFileSignature signer = new PdfFileSignature())
                {
                    signer.BindPdf(inputPdfPath);

                    // Configure the certificate used for signing.
                    signer.SetCertificate(certificatePath, certificatePassword);

                    // Optional: set a visual appearance for the signature.
                    // signer.SignatureAppearance = "signature_appearance.png";

                    // Perform the signature.
                    // Parameters: page, reason, contact, location, visibility, rectangle.
                    signer.Sign(
                        pageNumber,
                        "Document signed",
                        "contact@example.com",
                        "Office",
                        true,
                        signatureRect);

                    // Save the signed PDF.
                    signer.Save(outputPdfPath);
                }

                // If we reach this point the operation succeeded.
                break;
            }
            catch (IOException ex) when (IsFileLockException(ex))
            {
                // The file is locked – retry after a delay.
                attempt++;
                if (attempt >= maxRetries)
                {
                    // Exceeded retry count – rethrow the exception.
                    throw new IOException(
                        $"Failed to sign the PDF after {maxRetries} attempts because the file remains locked.", ex);
                }

                // Wait before the next attempt.
                Thread.Sleep(delayMilliseconds);
            }
        }
    }

    // Determines whether an IOException is caused by a file lock.
    private static bool IsFileLockException(IOException ex)
    {
        // Common message when a file is locked by another process.
        return ex.Message.IndexOf("being used by another process", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    // Example usage.
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certFile = "certificate.pfx";
        const string certPassword = "password";

        // Define the signature rectangle (x, y, width, height) using System.Drawing.Rectangle.
        var rect = new System.Drawing.Rectangle(100, 100, 200, 200);

        try
        {
            SignPdfWithRetry(
                inputPdf,
                outputPdf,
                certFile,
                certPassword,
                pageNumber: 1,
                signatureRect: rect);
            Console.WriteLine($"PDF signed successfully and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error signing PDF: {ex.Message}");
        }
    }
}