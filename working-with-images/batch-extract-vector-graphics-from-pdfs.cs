using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input directory containing PDF files
        const string inputDirectory = @"C:\InputPdfs";
        // Base output directory where each PDF's SVGs will be stored in a separate subfolder
        const string outputBaseDirectory = @"C:\ExtractedVectorGraphics";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure the base output directory exists
        Directory.CreateDirectory(outputBaseDirectory);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Create a dedicated folder for the current PDF file
                string pdfFileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string outputFolder = Path.Combine(outputBaseDirectory, pdfFileNameWithoutExt);
                Directory.CreateDirectory(outputFolder);

                // Load the PDF document (lifecycle rule: use using for deterministic disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate through pages (Aspose.Pdf uses 1‑based indexing)
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Check if the page contains vector graphics
                        if (page.HasVectorGraphics())
                        {
                            // Build the SVG file path for this page
                            string svgFilePath = Path.Combine(outputFolder, $"Page_{pageIndex}.svg");

                            // Try to save the vector graphics as SVG
                            bool saved = page.TrySaveVectorGraphics(svgFilePath);
                            if (saved)
                            {
                                Console.WriteLine($"Extracted SVG from '{pdfPath}' page {pageIndex} to '{svgFilePath}'.");
                            }
                            else
                            {
                                Console.WriteLine($"Failed to extract SVG from '{pdfPath}' page {pageIndex}.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Vector graphics extraction completed.");
    }
}