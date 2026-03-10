using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked_output.pdf";
        const string watermarkImagePath = "watermark.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use fully‑qualified System.Drawing.Image to avoid ambiguity with Aspose.Pdf.Drawing.Image
            using (System.Drawing.Image wmImg = System.Drawing.Image.FromFile(watermarkImagePath))
            {
                // Define the position and size of the watermark on the page
                // Example: place it at (100,100) with size 200x100
                Aspose.Pdf.Rectangle wmRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

                // Apply the watermark to each page
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    page.Watermark = new Watermark(wmImg, wmRect);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}