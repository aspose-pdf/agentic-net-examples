using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        // Pages on which to add the figure annotation (1‑based indexing)
        int[] pagesToAnnotate = { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            foreach (int pageNumber in pagesToAnnotate)
            {
                // Guard against invalid page numbers
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.WriteLine($"Skipping invalid page number: {pageNumber}");
                    continue;
                }

                Page page = doc.Pages[pageNumber];

                // Define the rectangle where the annotation will appear.
                // Fully qualify to avoid ambiguity with System.Drawing.Rectangle.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Use a concrete subclass of CommonFigureAnnotation, e.g., SquareAnnotation.
                SquareAnnotation figure = new SquareAnnotation(page, rect)
                {
                    // Set the border color (annotation's own Color property)
                    Color = Aspose.Pdf.Color.Blue
                };

                // Set line width via the Border object (requires the parent annotation in ctor)
                figure.Border = new Border(figure) { Width = 2 };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(figure);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure annotations added and saved to '{outputPath}'.");
    }
}