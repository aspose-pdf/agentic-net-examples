using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf; // Document, Save, Decrypt
// Note: CertificateEncryptionOptions resides in Aspose.Pdf.Security, but the namespace does not exist for a using directive.
// Use the fully qualified name instead.

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "encrypted_certificate.pdf";
        const string outputPdfPath = "decrypted.pdf";
        const string publicCertPath = "public_certificate.cer";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the public certificate file exists
        if (!File.Exists(publicCertPath))
        {
            Console.Error.WriteLine($"Public certificate not found: {publicCertPath}");
            return;
        }

        // Create CertificateEncryptionOptions to access the private key from the hardware token.
        // The StoreName and StoreLocation specify where the private key is stored (e.g., My store of the current user).
        Aspose.Pdf.Security.CertificateEncryptionOptions certOptions = new Aspose.Pdf.Security.CertificateEncryptionOptions(
            publicCertPath,
            StoreName.My,
            StoreLocation.CurrentUser);

        // Open the encrypted PDF using the certificate options, decrypt it, and save the result.
        using (Document doc = new Document(inputPdfPath, certOptions))
        {
            // Decrypt the document (no parameters needed).
            doc.Decrypt();

            // Save the decrypted PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Decrypted PDF saved to '{outputPdfPath}'.");
    }
}