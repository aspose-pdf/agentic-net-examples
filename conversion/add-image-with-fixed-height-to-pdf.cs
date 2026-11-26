using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Output PDF file name
        const string outputPath = "heightAfterFile.pdf";

        // Path to the image file to be added (replace with an actual file path)
        const string imagePath = "sample.jpg";

        // Ensure the image file exists – create a simple placeholder if it does not.
        if (!File.Exists(imagePath))
        {
            using (var bmp = new Bitmap(200, 200))
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.LightGray);
                using (var font = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Point))
                {
                    g.DrawString("Sample", font, Brushes.Black, new PointF(20, 80));
                }
                bmp.Save(imagePath, ImageFormat.Jpeg);
            }
        }

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a new blank page to the document
            Page page = doc.Pages.Add();

            // Create an Aspose.Pdf.Image object, assign the source file and fix its height to 150 points
            Aspose.Pdf.Image img = new Aspose.Pdf.Image
            {
                File = imagePath,
                FixHeight = 150
            };

            // Add the image to the page's content
            page.Paragraphs.Add(img);

            // Save the document as a PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
