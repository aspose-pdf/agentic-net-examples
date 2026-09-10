using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class BatchVectorGraphicsExtractor
{
    static void Main()
    {
        // Base directory of the running application.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Folder containing the PDF files to process.
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: '{inputFolder}'. No PDFs will be processed.");
            return;
        }

        // Root folder where extracted SVG files will be stored.
        string outputRoot = Path.Combine(baseDir, "ExtractedVectors");
        Directory.CreateDirectory(outputRoot);

        // Get all PDF files in the input folder.
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found (skipping): {pdfPath}");
                continue;
            }

            try
            {
                // Create a dedicated sub‑folder for this PDF.
                string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
                string pdfOutputFolder = Path.Combine(outputRoot, pdfName);
                Directory.CreateDirectory(pdfOutputFolder);

                // Load the PDF document (wrapped in a using block for deterministic disposal).
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Check if the page contains vector graphics.
                        if (page.HasVectorGraphics())
                        {
                            // Create a folder for the current page's SVG files.
                            string pageFolder = Path.Combine(pdfOutputFolder, $"Page_{pageIndex}");
                            Directory.CreateDirectory(pageFolder);

                            // Use SvgExtractor to extract all vector graphics from the page into the folder.
                            SvgExtractor extractor = new SvgExtractor();
                            extractor.Extract(page, pageFolder);
                        }
                    }
                }

                Console.WriteLine($"Extracted vectors from '{pdfPath}' to '{pdfOutputFolder}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch extraction completed.");
    }
}
