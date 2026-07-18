using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermark_aligned.pdf";
        const double margin = 50; // uniform margin from each page edge

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate through all annotations on the page
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];

                    // Process only WatermarkAnnotation instances
                    if (ann is WatermarkAnnotation watermark)
                    {
                        // Compute a new rectangle aligned with the page margins
                        Aspose.Pdf.Rectangle media = page.MediaBox;
                        Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(
                            media.LLX + margin,   // left
                            media.LLY + margin,   // bottom
                            media.URX - margin,   // right
                            media.URY - margin    // top
                        );

                        // Apply the new rectangle
                        watermark.Rect = newRect;
                    }
                }
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark annotations aligned and saved to '{outputPath}'.");
    }
}