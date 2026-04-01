using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";
        const int maxAttempts = 5;
        const int delayMilliseconds = 500;

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
        bool signed = false;
        while (attempt < maxAttempts && !signed)
        {
            try
            {
                using (Document doc = new Document(inputPath))
                {
                    // Initialize the signature facade with the document
                    PdfFileSignature pdfSignature = new PdfFileSignature(doc);
                    // Set the certificate for signing
                    pdfSignature.SetCertificate(certificatePath, certificatePassword);
                    // Create a PKCS7 signature object (inherits from Signature)
                    PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword);
                    // Define the visible signature rectangle (System.Drawing.Rectangle)
                    System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 50);
                    // Sign page 1, make the signature visible
                    pdfSignature.Sign(1, true, signatureRect, pkcs7Signature);
                    // Save the signed PDF
                    pdfSignature.Save(outputPath);
                }
                signed = true;
                Console.WriteLine($"PDF signed successfully and saved to '{outputPath}'.");
            }
            catch (IOException ioEx)
            {
                // Likely the file is locked by another process
                attempt++;
                Console.WriteLine($"Attempt {attempt} failed due to I/O error: {ioEx.Message}");
                if (attempt >= maxAttempts)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Signing aborted.");
                    break;
                }
                Thread.Sleep(delayMilliseconds);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                break;
            }
        }
    }
}
