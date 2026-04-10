using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf; // Document, etc.

class Program
{
    static void Main()
    {
        const string inputPdf  = "encrypted_certificate.pdf";
        const string outputPdf = "decrypted.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the public certificate (with private key) from the hardware token / store.
        // Adjust StoreName/StoreLocation as needed for your token.
        X509Certificate2 publicCert;
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            // Here we simply take the first certificate; replace with proper selection logic.
            if (store.Certificates.Count == 0)
            {
                Console.Error.WriteLine("No certificates found in the specified store.");
                return;
            }
            publicCert = store.Certificates[0];
        }

        // Create certificate‑based encryption options.
        Aspose.Pdf.Security.CertificateEncryptionOptions certOptions = new Aspose.Pdf.Security.CertificateEncryptionOptions(
            publicCert,
            StoreName.My,
            StoreLocation.CurrentUser);

        // Open the PDF using the certificate options, decrypt, and save.
        using (Document doc = new Document(inputPdf, certOptions))
        {
            doc.Decrypt();                 // Decrypt the document.
            doc.Save(outputPdf);            // Save the decrypted version.
        }

        Console.WriteLine($"Decrypted PDF saved to '{outputPdf}'.");
    }
}