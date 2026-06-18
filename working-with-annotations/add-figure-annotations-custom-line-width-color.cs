using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        // Pages on which to place the figure annotations (1‑based indexing)
        int[] targetPages = { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document‑disposal‑with‑using)
        using (Document doc = new Document(inputPath))
        {
            // Define annotation appearance
            Aspose.Pdf.Color annotationColor = Aspose.Pdf.Color.Blue;   // Stroke color
            int lineWidth = 2;                                          // Custom line width (int, as Border.Width expects int)

            // Iterate over the selected pages
            foreach (int pageNumber in targetPages)
            {
                // Guard against invalid page numbers
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.WriteLine($"Skipping invalid page number: {pageNumber}");
                    continue;
                }

                Page page = doc.Pages[pageNumber];

                // Define the rectangle that bounds the figure annotation
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

                // Create a concrete figure annotation (SquareAnnotation)
                SquareAnnotation figure = new SquareAnnotation(page, rect)
                {
                    // Set the stroke color
                    Color = annotationColor
                };

                // Set custom line width via the Border object (requires parent annotation)
                figure.Border = new Border(figure) { Width = lineWidth };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(figure);
            }

            // Save the modified PDF (using rule: document‑disposal‑with‑using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure annotations added and saved to '{outputPath}'.");
    }
}
