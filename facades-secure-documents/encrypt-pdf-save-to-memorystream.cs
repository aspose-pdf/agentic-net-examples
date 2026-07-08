using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Create a PDF document entirely in memory – no external file required.
        using (Document doc = new Document())
        {
            // Add a single page with some sample content.
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample PDF for encryption"));

            // Define the permissions that will be allowed after encryption.
            Permissions perms = Permissions.PrintDocument |
                                 Permissions.ModifyContent |
                                 Permissions.ExtractContent |
                                 Permissions.AssembleDocument;

            // Encrypt the PDF using AES‑128 algorithm.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);

            // Save the encrypted PDF into a MemoryStream for transmission.
            using (MemoryStream encryptedStream = new MemoryStream())
            {
                doc.Save(encryptedStream);
                Console.WriteLine($"Encrypted PDF size: {encryptedStream.Length} bytes");
                // Reset the position if the stream will be read/sent later.
                encryptedStream.Position = 0;
                // The stream is now ready to be sent over a network connection.
            }
        }
    }
}
