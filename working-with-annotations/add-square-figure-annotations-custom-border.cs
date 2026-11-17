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

        // Pages on which the figure annotation should be added (1‑based indexing)
        int[] selectedPages = { 1, 3, 5 };

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define common annotation properties
            // Rectangle coordinates: lower‑left x, lower‑left y, upper‑right x, upper‑right y
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Custom border color (using Aspose.Pdf.Color, not System.Drawing.Color)
            Aspose.Pdf.Color borderColor = Aspose.Pdf.Color.FromRgb(0.0, 0.5, 1.0); // teal

            // Custom line width (border thickness)
            double lineWidth = 2.0;

            // Iterate over the selected pages (pages are 1‑based)
            foreach (int pageNumber in selectedPages)
            {
                // Guard against out‑of‑range page numbers
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.WriteLine($"Skipping invalid page number: {pageNumber}");
                    continue;
                }

                Page page = doc.Pages[pageNumber];

                // Use SquareAnnotation as a generic "figure" annotation.
                // Other figure types (CircleAnnotation, LineAnnotation, etc.) can be used similarly.
                SquareAnnotation figAnno = new SquareAnnotation(page, rect)
                {
                    Color = borderColor,
                    // The textual label for the annotation is stored in the Contents property.
                    Contents = $"Figure on page {pageNumber}"
                };

                // Set the border width (integer) – this controls the line thickness.
                figAnno.Border = new Border(figAnno) { Width = (int)lineWidth };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(figAnno);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure annotations added. Output saved to '{outputPath}'.");
    }
}
