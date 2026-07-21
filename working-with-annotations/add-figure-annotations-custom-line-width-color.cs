using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";
        // Output PDF path
        const string outputPath = "output.pdf";

        // Pages on which to add figure annotations (1‑based indexing)
        int[] targetPages = { 1, 3, 5 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            foreach (int pageNumber in targetPages)
            {
                // Guard against out‑of‑range page numbers
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    continue;

                Page page = doc.Pages[pageNumber];

                // Define the rectangle for the figure annotation
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

                // Create a concrete figure annotation (SquareAnnotation in this example)
                SquareAnnotation figure = new SquareAnnotation(page, rect)
                {
                    // Set the annotation color (stroke color)
                    Color = Aspose.Pdf.Color.Blue,
                    // Optional: set a title or contents
                    Title = "Sample Figure",
                    Contents = "Custom square annotation"
                };

                // Set custom line width via the Border object.
                // Border constructor requires the parent annotation instance.
                figure.Border = new Border(figure) { Width = 3 };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(figure);
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure annotations added and saved to '{outputPath}'.");
    }
}