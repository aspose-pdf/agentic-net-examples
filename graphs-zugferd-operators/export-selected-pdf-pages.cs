using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "ExportedPages";

        // Pages to export – use 1‑based indices as Aspose.Pdf expects.
        int[] pagesToExport = { 2, 4, 5 }; // example selection

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        // Load the source PDF. Document is wrapped in a using block (lifecycle rule).
        using (Document srcDoc = new Document(inputPath))
        {
            // Iterate over the requested page numbers.
            foreach (int pageNum in pagesToExport)
            {
                // Validate the page number against the source document.
                if (pageNum < 1 || pageNum > srcDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageNum} is out of range (1‑{srcDoc.Pages.Count}). Skipping.");
                    continue;
                }

                // Create a new PDF that will contain only this page.
                using (Document singlePageDoc = new Document())
                {
                    // Add the selected page. The page is cloned, preserving its size and orientation.
                    singlePageDoc.Pages.Add(srcDoc.Pages[pageNum]);

                    // Build the output file name.
                    string outPath = Path.Combine(outputFolder, $"Page_{pageNum}.pdf");

                    // Save the single‑page PDF.
                    singlePageDoc.Save(outPath);
                    Console.WriteLine($"Exported page {pageNum} to '{outPath}'.");
                }
            }
        }
    }
}