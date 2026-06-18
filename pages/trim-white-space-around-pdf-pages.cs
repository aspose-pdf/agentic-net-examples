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

                // Optional: skip completely blank pages (default threshold 0.0)
                if (page.IsBlank(0.0))
                    continue;

                // Calculate the bounding box of the actual content
                Aspose.Pdf.Rectangle contentBox = page.CalculateContentBBox();

                // Set the TrimBox to the content bounding box to remove surrounding white space
                page.TrimBox = contentBox;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Trimmed PDF saved to '{outputPath}'.");
    }
}