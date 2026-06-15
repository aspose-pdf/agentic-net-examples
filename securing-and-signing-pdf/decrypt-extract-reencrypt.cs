using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using System.Drawing;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample image file
        string imagePath = "sample.png";
        CreateSampleImage(imagePath);

        // Step 2: Create a sample PDF and embed the image
        string pdfPath = "input.pdf";
        using (Document pdfDoc = new Document())
        {
            Page page = pdfDoc.Pages.Add();
            Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
            pdfImage.File = imagePath;
            pdfImage.FixWidth = 200;
            pdfImage.FixHeight = 200;
            page.Paragraphs.Add(pdfImage);
            pdfDoc.Save(pdfPath);
        }

        // Step 3: Encrypt the PDF with initial passwords
        string encryptedPath = "encrypted.pdf";
        using (Document encDoc = new Document(pdfPath))
        {
            Permissions permissions = Permissions.PrintDocument;
            encDoc.Encrypt("user123", "owner123", permissions, CryptoAlgorithm.AESx128);
            encDoc.Save(encryptedPath);
        }

        // Step 4: Open encrypted PDF, decrypt, extract images, and re‑encrypt with new owner password
        string finalPath = "output.pdf";
        using (Document doc = new Document(encryptedPath, "user123"))
        {
            // Decrypt the document
            doc.Decrypt();

            // Extract images from each page
            int pageNumber = 1;
            foreach (Page page in doc.Pages)
            {
                int imageIndex = 1;
                foreach (XImage xImage in page.Resources.Images)
                {
                    string extractedImagePath = $"image_page{pageNumber}_img{imageIndex}.png";
                    using (FileStream imageStream = new FileStream(extractedImagePath, FileMode.Create, FileAccess.Write))
                    {
                        xImage.Save(imageStream);
                    }
                    imageIndex++;
                }
                pageNumber++;
            }

            // Re‑encrypt with new owner password (keep same user password)
            Permissions newPermissions = Permissions.PrintDocument;
            doc.Encrypt("user123", "newOwner456", newPermissions, CryptoAlgorithm.AESx128);
            doc.Save(finalPath);
        }

        // Optional cleanup of temporary files
        // File.Delete(imagePath);
        // File.Delete(pdfPath);
        // File.Delete(encryptedPath);
    }

    private static void CreateSampleImage(string path)
    {
        using (Bitmap bitmap = new Bitmap(200, 200))
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(System.Drawing.Color.Red);
            }
            bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}