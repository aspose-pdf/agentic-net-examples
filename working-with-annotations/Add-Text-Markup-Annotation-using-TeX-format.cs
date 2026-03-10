using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the markup will appear (coordinates in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a concrete TextMarkupAnnotation – HighlightAnnotation in this case
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                Color    = Aspose.Pdf.Color.Yellow,   // visual highlight colour
                Contents = "TeX formula annotation"   // tooltip text
            };

            // Prepare a TeX fragment – this is just the raw LaTeX source string
            TeXFragment tex = new TeXFragment(@"\frac{a}{b}");

            // Store the TeX source in the RichText property (displayed in the popup)
            highlight.RichText = tex.ToString();

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with TeX markup annotation to '{outputPath}'.");
    }
}