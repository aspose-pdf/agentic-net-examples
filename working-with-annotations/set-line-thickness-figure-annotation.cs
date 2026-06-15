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

        // Load the existing PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a concrete figure annotation (SquareAnnotation is a subclass of CommonFigureAnnotation)
            SquareAnnotation figure = new SquareAnnotation(page, rect);

            // Set the line thickness (border width) to 2 points.
            // Border requires the parent annotation in its constructor.
            figure.Border = new Border(figure) { Width = 2 };

            // Optional: set a visible color for the border
            figure.Color = Aspose.Pdf.Color.Blue;

            // Add the annotation to the page
            page.Annotations.Add(figure);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure annotation with 2‑point line thickness saved to '{outputPath}'.");
    }
}