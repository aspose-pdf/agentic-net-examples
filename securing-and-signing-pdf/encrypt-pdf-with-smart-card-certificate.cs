using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "encrypted.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Retrieve the certificate from the hardware security module / smart card.
        // Adjust the store name, location, and search criteria (e.g., thumbprint) as needed.
        X509Certificate2 certificate = null;
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            foreach (var cert in store.Certificates)
            {
                // Example: match by thumbprint (replace with actual thumbprint)
                if (cert.Thumbprint != null && cert.Thumbprint.Equals("YOUR_CERT_THUMBPRINT", StringComparison.OrdinalIgnoreCase))
                {
                    certificate = cert;
                    break;
                }
            }
            store.Close();
        }

        if (certificate == null)
        {
            Console.Error.WriteLine("Certificate not found in the smart card store.");
            return;
        }

        // Prepare the list of public certificates (one per recipient).
        IList<X509Certificate2> publicCertificates = new List<X509Certificate2> { certificate };

        // Define permissions for the encrypted PDF.
        Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

        // Encrypt the PDF using the certificate and AES-256 algorithm.
        using (Document doc = new Document(inputPdfPath))
        {
            doc.Encrypt(permissions, CryptoAlgorithm.AESx256, publicCertificates);
            doc.Save(outputPdfPath); // Saving as PDF (default format)
        }

        Console.WriteLine($"PDF encrypted successfully and saved to '{outputPdfPath}'.");
    }
}