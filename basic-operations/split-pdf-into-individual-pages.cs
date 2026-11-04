using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "large_document.pdf";
        const string outputRootFolder = "SplitPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRootFolder);

        // Open the source PDF inside a using block for deterministic disposal
        using (Document sourceDoc = new Document(inputPdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf (see global rule)
            for (int pageNumber = 1; pageNumber <= sourceDoc.Pages.Count; pageNumber++)
            {
                // Create a folder for the current page
                string pageFolder = Path.Combine(outputRootFolder, $"Page_{pageNumber}");
                Directory.CreateDirectory(pageFolder);

                // Create a new PDF containing only the current page
                using (Document singlePageDoc = new Document())
                {
                    // Add the page from the source document
                    singlePageDoc.Pages.Add(sourceDoc.Pages[pageNumber]);

                    // Save the single‑page PDF inside its folder
                    string outputPdfPath = Path.Combine(pageFolder, $"Page_{pageNumber}.pdf");
                    singlePageDoc.Save(outputPdfPath);
                }

                Console.WriteLine($"Saved page {pageNumber} to folder: {pageFolder}");
            }
        }

        Console.WriteLine("Batch split completed.");
    }
}