using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input folder containing PDF files
        string inputFolder = @"C:\InputPdfs";
        // Output folder for extracted SVG files
        string outputFolder = @"C:\ExtractedSvgs";

        // Verify that the input directory exists; if not, inform the user and exit gracefully.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: '{inputFolder}'. Please create the folder and place PDF files inside it.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Open the PDF document inside a using block for proper disposal
            using (Document doc = new Document(pdfPath))
            {
                // Iterate through pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Check if the page contains vector graphics
                    if (page.HasVectorGraphics())
                    {
                        // Build a unique SVG file name per page
                        string svgFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_page{pageIndex}.svg";
                        string svgPath = Path.Combine(outputFolder, svgFileName);

                        // Extract vector graphics to an SVG file
                        // TrySaveVectorGraphics returns true if extraction succeeded
                        bool extracted = page.TrySaveVectorGraphics(svgPath);

                        if (!extracted)
                        {
                            Console.Error.WriteLine($"Failed to extract SVG from page {pageIndex} of '{pdfPath}'.");
                        }
                    }
                }
            }

            Console.WriteLine($"Processed '{pdfPath}'.");
        }

        Console.WriteLine("Vector graphic extraction completed.");
    }
}
