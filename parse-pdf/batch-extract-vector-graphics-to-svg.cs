using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder = "InputPdfs";

        // Folder where all extracted SVG files will be placed
        const string outputFolder = "ExtractedSvgs";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document (lifecycle: create + load)
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Build a unique SVG file name for each page
                        string svgFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_page{pageIndex}.svg";
                        string svgFullPath = Path.Combine(outputFolder, svgFileName);

                        // Try to save vector graphics of the page to an SVG file
                        // Returns true if vector graphics were present and saved
                        bool saved = page.TrySaveVectorGraphics(svgFullPath);

                        if (saved)
                        {
                            Console.WriteLine($"Extracted SVG: {svgFullPath}");
                        }
                    }
                } // Document disposed here (lifecycle: dispose)
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch extraction of vector graphics completed.");
    }
}