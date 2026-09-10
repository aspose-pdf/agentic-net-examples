using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        // Define the pages on which the figure annotations will be added (1‑based indexing)
        int[] selectedPages = { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the lifecycle rule for disposal)
        using (Document doc = new Document(inputPath))
        {
            foreach (int pageNumber in selectedPages)
            {
                // Guard against out‑of‑range page numbers
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    continue;

                Page page = doc.Pages[pageNumber];

                // Define the rectangle for the figure annotation (coordinates in points)
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

                // Use a concrete subclass of CommonFigureAnnotation – SquareAnnotation in this case
                SquareAnnotation square = new SquareAnnotation(page, rect)
                {
                    // Set the annotation's border color
                    Color = Aspose.Pdf.Color.Blue
                };

                // Set custom line width via the Border object (requires the parent annotation in ctor)
                square.Border = new Border(square) { Width = 3 };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(square);
            }

            // Save the modified PDF (using the lifecycle rule for saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure annotations added and saved to '{outputPath}'.");
    }
}