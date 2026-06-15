using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddImageStampAlternativeTextExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF file.
            using (Document sampleDoc = new Document())
            {
                Page samplePage = sampleDoc.Pages.Add();
                TextFragment sampleText = new TextFragment("Sample PDF for image stamp");
                samplePage.Paragraphs.Add(sampleText);
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Create a simple image to be used as a stamp.
            string imagePath = "stamp.png";
            using (Bitmap bitmap = new Bitmap(100, 50))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // System.Drawing.Color is required for GDI+ drawing methods.
                    graphics.Clear(System.Drawing.Color.LightBlue);
                    using (System.Drawing.Font font = new System.Drawing.Font("Arial", 12))
                    {
                        graphics.DrawString("Logo", font, Brushes.Black, new PointF(10f, 15f));
                    }
                }
                bitmap.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            }

            // Step 3: Open the PDF and add the image stamp with Unicode alternative text.
            using (Document doc = new Document("input.pdf"))
            {
                Page page = doc.Pages[1]; // 1‑based indexing as required.
                ImageStamp imgStamp = new ImageStamp(imagePath);
                imgStamp.AlternativeText = "示例图像 - Example Image - مثال صورة";
                imgStamp.Width = 100;
                imgStamp.Height = 50;
                imgStamp.XIndent = 100;
                imgStamp.YIndent = 100;
                page.AddStamp(imgStamp);
                doc.Save("output.pdf");
            }
        }
    }
}
