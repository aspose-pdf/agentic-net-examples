using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace Aes256EncryptionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF
            using (Document document = new Document())
            {
                // Add a page
                Page page = document.Pages.Add();

                // Add some text
                TextFragment text = new TextFragment("This PDF will be encrypted with AES‑256.");
                page.Paragraphs.Add(text);

                // Save the sample PDF
                document.Save("input.pdf");
            }

            // Open the PDF and apply encryption
            using (Document encryptedDocument = new Document("input.pdf"))
            {
                // Set user and owner passwords, permissions, and AES‑256 algorithm
                string userPassword = "user123";
                string ownerPassword = "owner123";

                // Define permissions (allow printing only)
                Permissions permissions = Permissions.PrintDocument;

                // Apply encryption using AES‑256
                encryptedDocument.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                encryptedDocument.Save("encrypted.pdf");
            }
        }
    }
}
