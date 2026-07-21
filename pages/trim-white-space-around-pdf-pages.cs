using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "trimmed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Calculate the bounding box of the actual content on the page.
                // This method analyses the page content (including bitmap) and
                // returns the smallest rectangle that encloses all visible objects.
                Aspose.Pdf.Rectangle contentBox = page.CalculateContentBBox();

                // Set the TrimBox to the calculated content rectangle.
                // TrimBox defines the region to which the page should be trimmed.
                page.TrimBox = contentBox;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Trimmed PDF saved to '{outputPath}'.");
    }
}