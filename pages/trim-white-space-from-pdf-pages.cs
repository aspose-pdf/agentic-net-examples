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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Determine if the page is essentially blank.
                // Using a small fill threshold (default recommendation).
                bool isBlank = page.IsBlank(0.01);
                if (isBlank)
                    continue; // Skip blank pages

                // Calculate the bounding box of the actual content.
                // This returns a Rectangle in page coordinates.
                Aspose.Pdf.Rectangle contentBox = page.CalculateContentBBox();

                // Set the TrimBox to the content bounding box to trim white space.
                page.TrimBox = contentBox;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Trimmed PDF saved to '{outputPath}'.");
    }
}