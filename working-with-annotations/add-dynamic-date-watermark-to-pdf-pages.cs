using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Generate a dynamic date string for this page
                string dateString = DateTime.Now.ToString("yyyy-MM-dd");

                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    pageWidth - 150, // lower‑left X
                    30,              // lower‑left Y
                    pageWidth - 30,  // upper‑right X
                    80);             // upper‑right Y

                // Create the watermark annotation using the correct properties
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
                {
                    Contents = dateString,                                   // text of the watermark
                    Opacity = 0.5,
                    Color = Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.9) // text color (no BackgroundColor property)
                };

                page.Annotations.Add(watermark);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
