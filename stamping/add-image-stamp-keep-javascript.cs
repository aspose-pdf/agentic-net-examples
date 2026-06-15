using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

namespace AddImageStampKeepJavascriptExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // -----------------------------------------------------------------
            // Step 1: Create a sample image that will be used as a stamp.
            // -----------------------------------------------------------------
            string stampImagePath = "stamp.png";
            Bitmap bitmap = new Bitmap(100, 50);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(System.Drawing.Color.LightGray);
            System.Drawing.Font font = new System.Drawing.Font("Arial", 12);
            Brush brush = Brushes.Black;
            graphics.DrawString("Stamp", font, brush, new PointF(10f, 15f));
            bitmap.Save(stampImagePath, ImageFormat.Png);
            graphics.Dispose();
            font.Dispose();
            bitmap.Dispose();

            // -----------------------------------------------------------------
            // Step 2: Create a sample PDF that contains a JavaScript action.
            // -----------------------------------------------------------------
            using (Document sampleDoc = new Document())
            {
                Page samplePage = sampleDoc.Pages.Add();
                TextFragment text = new TextFragment("Sample PDF with JavaScript action.");
                samplePage.Paragraphs.Add(text);

                // Add a document‑level JavaScript action using OpenAction.
                sampleDoc.OpenAction = new JavascriptAction("app.alert('Hello from JavaScript!');");

                sampleDoc.Save("input.pdf");
            }

            // -----------------------------------------------------------------
            // Step 3: Open the PDF, add an image stamp to each page, and save.
            // -----------------------------------------------------------------
            using (Document pdfDoc = new Document("input.pdf"))
            {
                // Iterate through pages (evaluation mode allows up to 4 pages).
                foreach (Page page in pdfDoc.Pages)
                {
                    ImageStamp imageStamp = new ImageStamp(stampImagePath);
                    imageStamp.HorizontalAlignment = HorizontalAlignment.Right;
                    imageStamp.VerticalAlignment = VerticalAlignment.Bottom;
                    imageStamp.Opacity = 0.5f;
                    page.AddStamp(imageStamp);
                }

                // The JavaScript action added earlier is preserved automatically.
                pdfDoc.Save("output.pdf");
            }
        }
    }
}
