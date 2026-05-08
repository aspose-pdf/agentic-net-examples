using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";

        // Verify that the encrypted PDF exists before attempting to open it.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file '{inputPath}' was not found. Please place the encrypted PDF in the working directory or update the path.");
            return;
        }

        // Retrieve a certificate that contains a private key.
        // If you know the exact subject, replace the empty string with the desired value.
        X509Certificate2 cert = GetCertificateFromStore(string.Empty);

        // Create certificate‑based encryption options (fully qualified type)
        Aspose.Pdf.Security.CertificateEncryptionOptions certOptions =
            new Aspose.Pdf.Security.CertificateEncryptionOptions(cert, StoreName.My, StoreLocation.CurrentUser);

        // Open the PDF with the certificate options, decrypt, and save
        using (Document doc = new Document(inputPath, certOptions))
        {
            doc.Decrypt();                     // Decrypt the document
            doc.Save(outputPath);              // Save the decrypted PDF
        }

        Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
    }

    // Helper: locate a certificate with a private key in the current user's store.
    // If 'subjectContains' is null or empty, the first certificate with a private key is returned.
    static X509Certificate2 GetCertificateFromStore(string subjectContains)
    {
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection collection = store.Certificates;

            // Prefer certificates that have a private key.
            foreach (X509Certificate2 cert in collection)
            {
                if (!cert.HasPrivateKey)
                    continue;

                if (string.IsNullOrEmpty(subjectContains) ||
                    (!string.IsNullOrEmpty(cert.Subject) && cert.Subject.IndexOf(subjectContains, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    return cert;
                }
            }
        }

        // If we reach this point, no suitable certificate was found.
        throw new InvalidOperationException(
            string.IsNullOrEmpty(subjectContains)
                ? "No certificate with a private key was found in the current user's store."
                : $"Certificate containing subject '{subjectContains}' with a private key was not found in the current user's store.");
    }
}