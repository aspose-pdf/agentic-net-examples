using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class BatchVectorGraphicsExtractor
{
    static void Main()
    {
        // Input directory containing PDF files
        const string inputDirectory = @"C:\InputPdfs";
        // Output root directory where each PDF will have its own folder
        const string outputRoot = @"C:\ExtractedVectors";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure the output root exists
        Directory.CreateDirectory(outputRoot);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
            // Create a dedicated folder for this PDF's extracted graphics
            string pdfOutputFolder = Path.Combine(outputRoot, pdfName);
            Directory.CreateDirectory(pdfOutputFolder);

            try
            {
                // Load the PDF document (lifecycle rule: wrap in using)
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate pages using 1‑based indexing (global rule)
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Check if the page actually contains vector graphics
                        if (!page.HasVectorGraphics())
                            continue; // Skip pages without vector content

                        // Create a sub‑folder for this page's SVG files
                        string pageFolder = Path.Combine(pdfOutputFolder, $"Page_{pageIndex}");
                        Directory.CreateDirectory(pageFolder);

                        // Use SvgExtractor to write each vector graphic as an SVG file
                        SvgExtractor extractor = new SvgExtractor();
                        extractor.Extract(page, pageFolder);
                    }
                }

                Console.WriteLine($"Extracted vectors from '{pdfPath}' to '{pdfOutputFolder}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}