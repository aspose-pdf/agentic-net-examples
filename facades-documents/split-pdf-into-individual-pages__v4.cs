using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace included as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputFolder = "SplitPages";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the source PDF using Document (PdfFileEditor does not provide SplitToIndividual)
            using (Aspose.Pdf.Document sourceDoc = new Aspose.Pdf.Document(inputPath))
            {
                // Iterate over pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNumber = 1; pageNumber <= sourceDoc.Pages.Count; pageNumber++)
                {
                    // Create a new empty PDF document for the current page
                    using (Aspose.Pdf.Document singlePageDoc = new Aspose.Pdf.Document())
                    {
                        // Add the specific page from the source document
                        singlePageDoc.Pages.Add(sourceDoc.Pages[pageNumber]);

                        // Build output file name
                        string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.pdf");

                        // Save the single‑page PDF
                        singlePageDoc.Save(outputPath);
                        Console.WriteLine($"Saved page {pageNumber} → {outputPath}");
                    }
                }
            }

            Console.WriteLine("All pages have been split into individual PDFs.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during splitting: {ex.Message}");
        }
    }
}