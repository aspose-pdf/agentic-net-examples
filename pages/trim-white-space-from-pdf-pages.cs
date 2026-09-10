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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // If the page is considered blank (using the default threshold 0.0),
                // we can skip trimming to avoid setting an empty TrimBox.
                // The default threshold factor is 0.0 (any non‑zero content marks the page as non‑blank).
                if (page.IsBlank(0.0))
                    continue;

                // Calculate the bounding box of the actual content on the page.
                // This method analyses the page content (including vector graphics) and
                // returns the smallest rectangle that encloses everything.
                Aspose.Pdf.Rectangle contentBox = page.CalculateContentBBox();

                // Set the TrimBox to the content bounding box.
                // TrimBox defines the region of the page that should be retained after trimming.
                page.TrimBox = contentBox;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Trimmed PDF saved to '{outputPath}'.");
    }
}