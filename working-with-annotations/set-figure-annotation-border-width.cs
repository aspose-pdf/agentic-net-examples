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

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left X/Y, upper‑right X/Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a SquareAnnotation (a generic figure) on the first page
            SquareAnnotation squareAnno = new SquareAnnotation(page, rect);

            // Set the annotation's color (this controls the border color for figure annotations)
            squareAnno.Color = Aspose.Pdf.Color.Blue;

            // Set the border (line) thickness – Border requires the parent annotation in its constructor
            squareAnno.Border = new Border(squareAnno) { Width = 2 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(squareAnno);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with figure annotation to '{outputPath}'.");
    }
}
