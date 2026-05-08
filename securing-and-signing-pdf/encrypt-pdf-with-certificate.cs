using System;
using System.Collections.Generic;
using System.IO;
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

        // Load the public certificate that will be used for encryption.
        // The certificate is stored in the Windows certificate store (e.g., a smart‑card/HSM).
        X509Certificate2 publicCert;
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            // Adjust the find criteria as needed (e.g., by subject name, thumbprint, etc.).
            X509Certificate2Collection found = store.Certificates.Find(
                X509FindType.FindBySubjectName, "MySmartCardCert", false);

            if (found.Count == 0)
            {
                Console.Error.WriteLine("Public certificate not found in the store.");
                return;
            }

            publicCert = found[0];
        }

        // Prepare the list of recipient certificates (only one in this case).
        IList<X509Certificate2> recipientCertificates = new List<X509Certificate2> { publicCert };

        // Define the permissions that will be granted after decryption.
        Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

        // Encrypt the PDF using certificate‑based encryption (AES‑256).
        using (Document doc = new Document(inputPdfPath))
        {
            doc.Encrypt(permissions, CryptoAlgorithm.AESx256, recipientCertificates);
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF encrypted successfully. Output saved to '{outputPdfPath}'.");
    }
}