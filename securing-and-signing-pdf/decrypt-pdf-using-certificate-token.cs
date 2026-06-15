using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the encrypted PDF and the public certificate file.
        const string encryptedPdfPath = "encrypted.pdf";
        const string decryptedPdfPath = "decrypted.pdf";
        const string publicCertificatePath = "public.cer";

        // Verify that the input file exists.
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        // Verify that the public certificate file exists.
        if (!File.Exists(publicCertificatePath))
        {
            Console.Error.WriteLine($"Certificate not found: {publicCertificatePath}");
            return;
        }

        try
        {
            // Create CertificateEncryptionOptions that point to the public certificate.
            // The private key will be obtained from the hardware token (store) automatically.
            Aspose.Pdf.Security.CertificateEncryptionOptions certOptions = new Aspose.Pdf.Security.CertificateEncryptionOptions(
                publicCertificatePath,
                StoreName.My,               // Store where the private key resides (e.g., hardware token)
                StoreLocation.CurrentUser   // Store location
            );

            // Open the encrypted PDF using the certificate options.
            using (Document doc = new Document(encryptedPdfPath, certOptions))
            {
                // Decrypt the document. No password is needed because the certificate is used.
                doc.Decrypt();

                // Save the decrypted PDF.
                doc.Save(decryptedPdfPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{decryptedPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}