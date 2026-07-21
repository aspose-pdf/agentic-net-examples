using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class BatchVectorGraphicsExtractor
{
    static void Main()
    {
        // Directory containing the source PDF files
        const string inputDirectory = @"C:\PdfInput";

        // Base directory where extracted SVGs will be stored
        const string outputBaseDirectory = @"C:\PdfVectorGraphics";

        // Ensure the output base directory exists
        Directory.CreateDirectory(outputBaseDirectory);

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Folder name based on the PDF file name (without extension)
            string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputFolder = Path.Combine(outputBaseDirectory, pdfName);
            Directory.CreateDirectory(pdfOutputFolder);

            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Check if the page contains vector graphics
                    if (page.HasVectorGraphics())
                    {
                        // Create a sub‑folder for this page's SVG files
                        string pageFolder = Path.Combine(pdfOutputFolder, $"Page_{pageIndex}");
                        Directory.CreateDirectory(pageFolder);

                        // Extract all vector graphics on the page to separate SVG files
                        // SvgExtractor.Extract(Page, string) writes one SVG file per graphic element
                        SvgExtractor extractor = new SvgExtractor();
                        extractor.Extract(page, pageFolder);
                    }
                }
            }

            Console.WriteLine($"Extracted vector graphics from '{pdfPath}' to '{pdfOutputFolder}'.");
        }
    }
}