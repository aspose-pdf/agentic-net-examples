using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample image file
        string imagePath = "sample.png";
        using (Bitmap bitmap = new Bitmap(100, 100))
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(System.Drawing.Color.LightBlue);
                graphics.DrawEllipse(Pens.Red, 10, 10, 80, 80);
            }
            bitmap.Save(imagePath);
        }

        // Step 2: Create a PDF and embed the image
        using (Document pdfDoc = new Document())
        {
            pdfDoc.Pages.Add();
            Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
            pdfImage.File = imagePath;
            pdfDoc.Pages[1].Paragraphs.Add(pdfImage);
            pdfDoc.Save("encrypted.pdf");
        }

        // Step 3: Encrypt the PDF with a user password
        using (Document encryptedDoc = new Document("encrypted.pdf"))
        {
            Permissions permissions = new Permissions(); // default: all permissions allowed
            encryptedDoc.Encrypt("userpwd", "ownerpwd", permissions, CryptoAlgorithm.AESx128);
            encryptedDoc.Save("encrypted.pdf");
        }

        // Step 4: Open the encrypted PDF with the password and decrypt it
        using (Document protectedDoc = new Document("encrypted.pdf", "userpwd"))
        {
            protectedDoc.Decrypt();
            protectedDoc.Save("decrypted.pdf");
        }

        // Step 5: Extract all images from the decrypted PDF
        using (Document decryptedDoc = new Document("decrypted.pdf"))
        {
            int pageNumber = 1;
            foreach (Page page in decryptedDoc.Pages)
            {
                int imageIndex = 1;
                foreach (XImage xImg in page.Resources.Images)
                {
                    string outputImagePath = $"extracted_page{pageNumber}_img{imageIndex}.png";
                    using (FileStream fs = new FileStream(outputImagePath, FileMode.Create, FileAccess.Write))
                    {
                        xImg.Save(fs);
                    }
                    imageIndex++;
                }
                pageNumber++;
            }
        }
    }
}
