using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // Paths to the encrypted PDF and the public certificate (CER file)
        const string encryptedPdfPath = "encrypted.pdf";
        const string publicCertPath   = "public.cer";
        const string decryptedPdfPath = "decrypted.pdf";

        // Verify input files exist
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"Encrypted PDF not found: {encryptedPdfPath}");
            return;
        }
        if (!File.Exists(publicCertPath))
        {
            Console.Error.WriteLine($"Public certificate not found: {publicCertPath}");
            return;
        }

        // Create CertificateEncryptionOptions.
        // The private key is expected to be stored in the current user's "My" store
        // (e.g., a hardware token or smart card). The public certificate file is used
        // to locate the matching private key.
        CertificateEncryptionOptions certOptions = new CertificateEncryptionOptions(publicCertPath, StoreName.My, StoreLocation.CurrentUser);

        // Open the encrypted PDF using the certificate options.
        // The constructor overload (string filename, CertificateEncryptionOptions options)
        // handles certificate‑based encryption.
        using (Document doc = new Document(encryptedPdfPath, certOptions))
        {
            // Decrypt the document. No password is required because the certificate
            // provides the necessary decryption credentials.
            doc.Decrypt();

            // Save the decrypted PDF.
            doc.Save(decryptedPdfPath);
        }

        Console.WriteLine($"Decrypted PDF saved to '{decryptedPdfPath}'.");
    }
}