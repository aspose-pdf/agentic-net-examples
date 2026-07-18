using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";
        const string decryptedPdfPath = "decrypted.pdf";
        const string publicCertificatePath = "public.cer";

        // Verify that the required files exist before proceeding.
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"Encrypted PDF not found: {encryptedPdfPath}");
            return;
        }

        if (!File.Exists(publicCertificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {publicCertificatePath}");
            return;
        }

        // Load the public certificate from file. X509Certificate2 constructor accepts a path.
        X509Certificate2 publicCert = new X509Certificate2(publicCertificatePath);

        // Create encryption options that point to the store containing the private key
        // (e.g., a hardware token). The store name and location must match where the
        // token exposes the private key.
        var certOptions = new CertificateEncryptionOptions(
            publicCert,
            StoreName.My,               // Store that holds the private key
            StoreLocation.CurrentUser   // Store location
        );

        // Open the encrypted PDF with the certificate options, decrypt it, and save.
        using (var pdfDoc = new Document(encryptedPdfPath, certOptions))
        {
            pdfDoc.Decrypt();
            pdfDoc.Save(decryptedPdfPath);
        }

        Console.WriteLine($"Decrypted PDF saved to '{decryptedPdfPath}'.");
    }
}