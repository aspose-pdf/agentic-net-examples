using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace EncryptPdfExample
{
    class Program
    {
        static void Main()
        {
            // Step 1: Create a sample PDF and save it to a file
            using (Document sampleDoc = new Document())
            {
                Page page = sampleDoc.Pages.Add();
                TextFragment fragment = new TextFragment("Sample PDF for encryption");
                page.Paragraphs.Add(fragment);
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Load the PDF, encrypt it, and save to a memory stream
            using (Document encryptedDoc = new Document("input.pdf"))
            {
                Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;
                encryptedDoc.Encrypt("userPassword", "ownerPassword", permissions, CryptoAlgorithm.AESx256);

                using (MemoryStream outputStream = new MemoryStream())
                {
                    encryptedDoc.Save(outputStream);
                    // Reset the stream position to the beginning for further processing
                    outputStream.Position = 0;

                    // Example: display the size of the encrypted PDF
                    Console.WriteLine("Encrypted PDF size (bytes): " + outputStream.Length);
                }
            }
        }
    }
}
