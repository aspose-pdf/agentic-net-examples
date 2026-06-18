using System;
using System.IO;
using Aspose.Pdf;

class BatchVectorGraphicsExtractor
{
    static void Main(string[] args)
    {
        // Input directory containing PDF files.
        // You can pass the path as a command‑line argument or set it here.
        string inputDirectory = args.Length > 0 ? args[0] : @"C:\InputPdfs";

        // Base directory where each PDF's extracted graphics will be stored.
        string outputBaseDirectory = args.Length > 1 ? args[1] : @"C:\ExtractedGraphics";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDirectory}");
            return;
        }

        // Ensure the base output directory exists.
        Directory.CreateDirectory(outputBaseDirectory);

        // Process every PDF file in the input directory.
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputFolder = Path.Combine(outputBaseDirectory, pdfFileName);

            // Create a dedicated folder for this PDF's vector graphics.
            Directory.CreateDirectory(pdfOutputFolder);

            try
            {
                // Load the PDF document inside a using block for deterministic disposal.
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Build the SVG file name for the current page.
                        string svgFilePath = Path.Combine(pdfOutputFolder, $"Page_{pageIndex}.svg");

                        // Try to save vector graphics as SVG.
                        // Returns true if the page contains vector graphics; otherwise false.
                        bool extracted = page.TrySaveVectorGraphics(svgFilePath);

                        if (extracted)
                        {
                            Console.WriteLine($"Extracted vector graphics from '{pdfFileName}', page {pageIndex} → {svgFilePath}");
                        }
                        else
                        {
                            // No vector graphics on this page; optionally delete the empty file.
                            if (File.Exists(svgFilePath))
                            {
                                File.Delete(svgFilePath);
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

        Console.WriteLine("Batch extraction completed.");
    }
}