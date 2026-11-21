using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Pages (1‑based) from which graphics will be extracted
        int[] selectedPages = { 1, 3, 5 };

        try
        {
            // Document must be wrapped in a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate over the requested page numbers
                foreach (int pageNumber in selectedPages)
                {
                    // Validate page number using 1‑based indexing
                    if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    {
                        Console.WriteLine($"Page {pageNumber} is out of range, skipping.");
                        continue;
                    }

                    // Create a fresh GraphicsAbsorber for each page and visit the page
                    using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                    {
                        absorber.Visit(doc.Pages[pageNumber]);

                        // Output basic information about the extracted graphics
                        Console.WriteLine($"Page {pageNumber}: {absorber.Elements.Count} graphic objects extracted.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
