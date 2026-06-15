using System;
using Aspose.Pdf;

namespace EncryptZugferdPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF (simulating a ZUGFeRD PDF)
            using (Document sampleDoc = new Document())
            {
                // Add a blank page
                sampleDoc.Pages.Add();
                // Save as input.pdf
                sampleDoc.Save("input.pdf");
            }

            // Open the PDF and encrypt it with user and owner passwords
            using (Document doc = new Document("input.pdf"))
            {
                // Define permissions (no permissions). Cast 0 to Permissions to represent "none".
                Permissions permissions = (Permissions)0;
                // Choose a supported cryptographic algorithm (AES 128‑bit).
                CryptoAlgorithm algorithm = CryptoAlgorithm.AESx128;
                // Set passwords
                string userPassword = "user123";
                string ownerPassword = "owner123";

                // Encrypt the document
                doc.Encrypt(userPassword, ownerPassword, permissions, algorithm);
                // Save encrypted PDF
                doc.Save("output.pdf");
            }
        }
    }
}
