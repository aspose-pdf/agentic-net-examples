using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using System.Security.Cryptography.X509Certificates;

class Program
{
    // Determines whether the exception is caused by a file lock (sharing violation)
    static bool IsFileLocked(IOException ex)
    {
        // HResult 0x80070020 corresponds to ERROR_SHARING_VIOLATION
        const int sharingViolationHResult = unchecked((int)0x80070020);
        return ex.HResult == sharingViolationHResult || ex.Message.Contains("being used by another process");
    }

    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certPath       = "certificate.pfx";
        const string certPassword   = "certPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the signing certificate using the standard X509Certificate2 constructor
        X509Certificate2 certificate = new X509Certificate2(certPath, certPassword);

        const int maxRetries = 5;
        const int delayMilliseconds = 1000;
        int attempt = 0;
        bool signed = false;

        while (attempt < maxRetries && !signed)
        {
            attempt++;
            try
            {
                using (PdfFileSignature pdfSignature = new PdfFileSignature())
                {
                    // Bind the source PDF
                    pdfSignature.BindPdf(inputPdfPath);

                    // Create a PKCS7 object from the certificate (Aspose.Pdf.Forms namespace)
                    PKCS7 pkcs7 = new PKCS7(certPath, certPassword);

                    // Sign the document and write to the output path
                    pdfSignature.Sign(outputPdfPath, pkcs7);
                }

                signed = true;
                Console.WriteLine($"PDF signed successfully on attempt {attempt}.");
            }
            catch (IOException ioEx) when (IsFileLocked(ioEx))
            {
                // The file is locked; wait and retry
                Console.WriteLine($"Attempt {attempt}: PDF file is locked. Retrying in {delayMilliseconds} ms...");
                if (attempt == maxRetries)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Signing failed.");
                    throw;
                }
                Thread.Sleep(delayMilliseconds);
            }
            catch (Exception ex)
            {
                // Unexpected error – abort
                Console.Error.WriteLine($"Signing failed: {ex.Message}");
                throw;
            }
        }
    }
}
