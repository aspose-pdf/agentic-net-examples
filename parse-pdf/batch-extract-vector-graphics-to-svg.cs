using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder = "InputPdfs";

        // Folder where all extracted SVG files will be saved
        const string outputFolder = "ExtractedSvgs";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string pdfBaseName = Path.GetFileNameWithoutExtension(pdfPath);

            try
            {
                // Load the PDF document (wrapped in using for deterministic disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Check if the page contains vector graphics
                        if (page.HasVectorGraphics())
                        {
                            // Build a unique SVG file name per page
                            string svgFilePath = Path.Combine(
                                outputFolder,
                                $"{pdfBaseName}_page{pageIndex}.svg");

                            // Try to save the vector graphics as SVG
                            bool saved = page.TrySaveVectorGraphics(svgFilePath);

                            if (saved)
                                Console.WriteLine($"Extracted SVG: {svgFilePath}");
                            else
                                Console.WriteLine($"No vector graphics saved for {pdfBaseName} page {pageIndex}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}