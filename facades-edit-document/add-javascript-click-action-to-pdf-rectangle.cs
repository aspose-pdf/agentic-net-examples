using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle area for the annotation (llx, lly, urx, ury)
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a square (rectangular) annotation
            var annot = new SquareAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Red
            };

            // Set the border – the Border constructor requires the parent annotation
            annot.Border = new Border(annot)
            {
                Width = 2,
                Style = BorderStyle.Solid
            };

            // NOTE: Older Aspose.Pdf versions do not expose an Action property on
            // SquareAnnotation nor the JavascriptAction class. If you need a JavaScript
            // action attached to the annotation, upgrade to a newer Aspose.Pdf version
            // that supports it, or use document/page level JavaScript actions instead.

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annot);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rectangle annotation to '{outputPath}'.");
    }
}
