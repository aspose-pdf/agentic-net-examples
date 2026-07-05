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

        // Pages on which to add the figure annotation (1‑based indexing)
        int[] targetPages = { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            foreach (int pageNumber in targetPages)
            {
                // Guard against out‑of‑range page numbers
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    continue;

                Page page = doc.Pages[pageNumber];

                // Define the annotation rectangle (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a concrete figure annotation – SquareAnnotation in this case
                SquareAnnotation square = new SquareAnnotation(page, rect)
                {
                    // Set the annotation border color
                    Color = Aspose.Pdf.Color.Blue
                };

                // Set line width via Border (requires the parent annotation in the constructor)
                square.Border = new Border(square) { Width = 3 };

                // Add the annotation to the page
                page.Annotations.Add(square);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure annotations added and saved to '{outputPath}'.");
    }
}