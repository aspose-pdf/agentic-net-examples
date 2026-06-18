using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddImageStampEncryptedPdfExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a simple PDF file (self‑contained sample)
            using (Document createDoc = new Document())
            {
                Page createPage = createDoc.Pages.Add();
                TextFragment tf = new TextFragment("Sample PDF");
                createPage.Paragraphs.Add(tf);
                createDoc.Save("input.pdf");
            }

            // Step 2: Encrypt the PDF with a user password
            using (Document encryptDoc = new Document("input.pdf"))
            {
                // Use only the permissions that are guaranteed to exist in all versions
                Permissions permissions = Permissions.PrintDocument;

                CryptoAlgorithm algorithm = CryptoAlgorithm.AESx128;
                encryptDoc.Encrypt("userpwd", "ownerpwd", permissions, algorithm);
                encryptDoc.Save("encrypted.pdf");
            }

            // Step 3: Prepare a tiny PNG image (1x1 pixel) as a byte array
            string base64Image = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XcZcAAAAASUVORK5CYII=";
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            // Step 4: Open the encrypted PDF with the password, add an image stamp, and save
            using (MemoryStream imgStream = new MemoryStream(imageBytes))
            {
                using (Document stampedDoc = new Document("encrypted.pdf", "userpwd"))
                {
                    ImageStamp imgStamp = new ImageStamp(imgStream);
                    imgStamp.XIndent = 100;
                    imgStamp.YIndent = 100;
                    imgStamp.Width = 50;
                    imgStamp.Height = 50;

                    // Add the stamp to each page (Page.AddStamp, not collection level)
                    foreach (Page page in stampedDoc.Pages)
                    {
                        page.AddStamp(imgStamp);
                    }

                    stampedDoc.Save("output.pdf");
                }
            }
        }
    }
}
