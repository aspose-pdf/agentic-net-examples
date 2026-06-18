using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;

namespace RotateImageStampExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF file
            using (Document sampleDoc = new Document())
            {
                Page samplePage = sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Create a sample image to be used as a stamp
            using (Bitmap bitmap = new Bitmap(100, 100))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(System.Drawing.Color.LightBlue);
                    using (Pen pen = new Pen(System.Drawing.Color.Red, 2))
                    {
                        graphics.DrawEllipse(pen, 10, 10, 80, 80);
                    }
                }
                bitmap.Save("stamp.png");
            }

            // Step 3: Open the PDF, add a rotated image stamp, and save the result
            using (Document pdfDoc = new Document("input.pdf"))
            {
                Page targetPage = pdfDoc.Pages[1];
                ImageStamp imageStamp = new ImageStamp("stamp.png");
                // Rotate the stamp by 45 degrees (arbitrary angle)
                imageStamp.RotateAngle = 45;
                // Position the stamp (optional)
                imageStamp.XIndent = 100;
                imageStamp.YIndent = 100;
                // Add the stamp to the page
                targetPage.AddStamp(imageStamp);
                pdfDoc.Save("output.pdf");
            }
        }
    }
}
