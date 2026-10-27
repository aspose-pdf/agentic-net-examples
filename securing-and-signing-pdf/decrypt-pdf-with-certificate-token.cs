using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";
        const string certThumbprint = "YOUR_CERT_THUMBPRINT"; // replace with actual thumbprint

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the certificate (with private key) from the hardware token / smart card
        X509Certificate2 cert = GetCertificateFromStore(certThumbprint);
        if (cert == null)
        {
            Console.Error.WriteLine("Certificate not found or does not contain a private key.");
            return;
        }

        try
        {
            // Prepare options that tell Aspose.Pdf how to use the certificate for decryption
            var certOptions = new CertificateEncryptionOptions(cert, StoreName.My, StoreLocation.CurrentUser);

            // Open the encrypted PDF using a stream and the certificate options
            using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            using (Document doc = new Document(fs, certOptions))
            {
                // Remove encryption
                doc.Decrypt();

                // Save the decrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Retrieves a certificate with a private key from the current user's personal store
    static X509Certificate2 GetCertificateFromStore(string thumbprint)
    {
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            foreach (X509Certificate2 cert in store.Certificates)
            {
                if (string.Equals(cert.Thumbprint, thumbprint, StringComparison.OrdinalIgnoreCase) && cert.HasPrivateKey)
                {
                    return cert;
                }
            }
        }
        return null;
    }
}