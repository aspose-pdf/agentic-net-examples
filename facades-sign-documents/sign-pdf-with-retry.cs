using System;
using System.IO;
using System.Threading;
using System.Drawing;
using Aspose.Pdf.Facades;

namespace PdfSigningDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Simple demonstration. If the required arguments are supplied, sign the PDF; otherwise show usage.
            if (args.Length == 5)
            {
                string inputPdf = args[0];
                string outputPdf = args[1];
                string certPath = args[2];
                string certPassword = args[3];
                if (!int.TryParse(args[4], out int pageNumber))
                {
                    Console.WriteLine("Invalid page number.");
                    return;
                }

                Rectangle rect = new Rectangle(100, 100, 200, 100);
                PdfSigner.SignPdfWithRetry(inputPdf, outputPdf, certPath, certPassword, pageNumber, rect);
                Console.WriteLine("PDF signed successfully.");
            }
            else
            {
                Console.WriteLine("Usage: PdfSigningDemo <inputPdf> <outputPdf> <certPath> <certPassword> <pageNumber>");
            }
        }
    }

    public static class PdfSigner
    {
        // Signs a PDF file with a digital certificate.
        // If the file is locked by another process, the method retries the operation.
        public static void SignPdfWithRetry(
            string inputPdfPath,
            string outputPdfPath,
            string certificatePath,
            string certificatePassword,
            int pageNumber,
            Rectangle signatureRect,
            int maxRetryAttempts = 3,
            int retryDelayMilliseconds = 1000)
        {
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

            if (!File.Exists(certificatePath))
                throw new FileNotFoundException($"Certificate file not found: {certificatePath}");

            int attempt = 0;
            bool signed = false;

            // PdfFileSignature implements IDisposable via SaveableFacade, so we use a using block.
            using (PdfFileSignature pdfSigner = new PdfFileSignature())
            {
                // Bind the source PDF file.
                pdfSigner.BindPdf(inputPdfPath);

                // Set the certificate used for signing.
                pdfSigner.SetCertificate(certificatePath, certificatePassword);

                while (attempt < maxRetryAttempts && !signed)
                {
                    try
                    {
                        // Perform the signing operation.
                        pdfSigner.Sign(
                            pageNumber,
                            "Document signed",          // Reason
                            "Signer Contact",           // Contact
                            "Signing Location",         // Location
                            true,                        // Visible signature
                            signatureRect);

                        // Save the signed PDF to the output path.
                        pdfSigner.Save(outputPdfPath);
                        signed = true; // Success
                    }
                    catch (IOException ioEx) when (IsFileLockException(ioEx))
                    {
                        // The PDF file is locked; wait and retry.
                        attempt++;
                        if (attempt >= maxRetryAttempts)
                            throw new IOException($"Failed to sign PDF after {maxRetryAttempts} attempts due to file lock.", ioEx);

                        Thread.Sleep(retryDelayMilliseconds);
                    }
                }

                // Close the facade (optional, as using will dispose it).
                pdfSigner.Close();
            }
        }

        // Determines whether the IOException is caused by a file lock.
        private static bool IsFileLockException(IOException ex)
        {
            // On Windows, the HResult 0x20 (32) indicates "The process cannot access the file because it is being used by another process."
            const int ERROR_SHARING_VIOLATION = unchecked((int)0x80070020);
            return ex.HResult == ERROR_SHARING_VIOLATION;
        }
    }
}
