using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted_cert.pdf";
        const string decryptedPdfPath = "decrypted.pdf";
        const string certThumbprint   = "YOUR_CERT_THUMBPRINT"; // replace with actual thumbprint

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        // Locate the certificate stored on the hardware token (e.g., in the CurrentUser "My" store)
        X509Certificate2 cert = FindCertificateByThumbprint(certThumbprint);
        if (cert == null)
        {
            Console.Error.WriteLine("Certificate not found on the token.");
            return;
        }

        // Create encryption options that use the certificate (Aspose.Pdf.Security.CertificateEncryptionOptions)
        var certOptions = new CertificateEncryptionOptions(cert, StoreName.My, StoreLocation.CurrentUser);

        // Open the encrypted PDF using the certificate and save it unencrypted
        using (FileStream fs = new FileStream(encryptedPdfPath, FileMode.Open, FileAccess.Read))
        using (Document doc = new Document(fs, certOptions))
        {
            // The document is already opened with the private key, just save it without encryption
            doc.Save(decryptedPdfPath);
        }

        Console.WriteLine($"Decrypted PDF saved to '{decryptedPdfPath}'.");
    }

    // Helper: locate a certificate in the CurrentUser "My" store by thumbprint
    private static X509Certificate2 FindCertificateByThumbprint(string thumbprint)
    {
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            foreach (var cert in store.Certificates)
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
