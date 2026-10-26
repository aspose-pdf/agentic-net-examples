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
        const double margin = 50; // points from each edge

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

                // Annotations collection is also 1‑based
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    // Retrieve the annotation
                    var annotation = page.Annotations[j];

                    // Process only WatermarkAnnotations
                    if (annotation is WatermarkAnnotation watermark)
                    {
                        // Compute new rectangle respecting the desired margins
                        double llx = margin;                                 // lower‑left X
                        double lly = margin;                                 // lower‑left Y
                        double urx = page.PageInfo.Width  - margin;          // upper‑right X
                        double ury = page.PageInfo.Height - margin;          // upper‑right Y

                        // Set the bounds of the watermark using the correct property
                        watermark.Rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                    }
                }
            }

            // Save the modified PDF (extension determines format; no SaveOptions needed for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark annotations aligned and saved to '{outputPath}'.");
    }
}
