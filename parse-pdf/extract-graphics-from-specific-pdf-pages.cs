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

        // Define the page numbers to process (1‑based indexing)
        int[] pagesToExtract = { 2, 4, 5 };

        try
        {
            // Document must be disposed deterministically
            using (Document doc = new Document(inputPath))
            {
                // Ensure requested pages exist
                foreach (int pageNum in pagesToExtract)
                {
                    if (pageNum < 1 || pageNum > doc.Pages.Count)
                    {
                        Console.WriteLine($"Page {pageNum} is out of range. Skipping.");
                        continue;
                    }

                    // CORRECT: 1‑based page indexing
                    Page page = doc.Pages[pageNum];

                    // GraphicsAbsorber extracts vector graphics from a page
                    using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                    {
                        // Visit the page with the absorber (GraphicsAbsorber implements IVisitor)
                        absorber.Visit(page);

                        // Report how many graphics were found
                        Console.WriteLine($"Page {pageNum}: extracted {absorber.Elements.Count} graphics.");

                        // Example: iterate over extracted graphics (optional)
                        // foreach (var element in absorber.Elements)
                        // {
                        //     // Process each GraphicElement as needed
                        // }
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
