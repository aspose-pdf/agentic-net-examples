using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the rectangle area to be highlighted (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a HighlightAnnotation on the specified page and rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect);

            // Set visual properties of the highlight
            highlight.Color   = Aspose.Pdf.Color.Yellow; // use Aspose.Pdf.Color (cross‑platform)
            highlight.Opacity = 0.5;                     // optional transparency
            highlight.Contents = "Highlighted text";

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}