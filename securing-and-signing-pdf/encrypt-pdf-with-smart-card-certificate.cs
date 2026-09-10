using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the certificate from the smart card / hardware security module.
        // Adjust the store name/location and search criteria as needed for your environment.
        X509Certificate2 certificate = null;
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            foreach (var cert in store.Certificates)
            {
                // Example: select by subject name; replace with appropriate logic.
                if (cert.Subject.Contains("YourSmartCardSubject"))
                {
                    certificate = cert;
                    break;
                }
            }
        }

        if (certificate == null)
        {
            Console.Error.WriteLine("Certificate not found on the smart card.");
            return;
        }

        // Encrypt the PDF for the holder of the selected certificate.
        using (Document doc = new Document(inputPath))
        {
            // Define desired permissions (example: allow printing and content extraction).
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Use a strong algorithm; AESx256 is recommended.
            doc.Encrypt(perms, CryptoAlgorithm.AESx256, new List<X509Certificate2> { certificate });

            // Save the encrypted PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}