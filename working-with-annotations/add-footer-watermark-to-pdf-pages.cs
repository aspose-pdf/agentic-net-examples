using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Footer rectangle (left, bottom, right, top)
                var rect = new Rectangle(50, 20, 550, 40);

                // Create a WatermarkAnnotation for this page
                var wm = new WatermarkAnnotation(page, rect)
                {
                    // Unique identifier – using the page number
                    Contents = $"Page {i}",
                    // Appearance settings (optional)
                    Color   = Color.Gray,
                    Opacity = 0.5,
                    // Place behind page content
                    ZIndex = -1
                };

                // Add the annotation to the page
                page.Annotations.Add(wm);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
