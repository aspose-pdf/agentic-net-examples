using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_strikeout.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a strike‑out annotation
            StrikeOutAnnotation strike = new StrikeOutAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Red,          // strike line color
                Contents = "Strikeout annotation"      // optional comment
            };

            // Add the annotation to the page
            page.Annotations.Add(strike);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Strike‑out annotation added. Saved to '{outputPath}'.");
    }
}