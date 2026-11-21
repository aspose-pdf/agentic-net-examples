using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class BatchVectorGraphicsExtractor
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder = @"C:\PdfInput";

        // Root folder where extracted SVGs will be stored
        const string outputRoot = @"C:\PdfVectorGraphics";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output root exists
        Directory.CreateDirectory(outputRoot);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputFolder = Path.Combine(outputRoot, pdfName);

            // Create a dedicated folder for this PDF's vector graphics
            Directory.CreateDirectory(pdfOutputFolder);

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate through each page and extract its vector graphics as SVG files
                    for (int pageNo = 1; pageNo <= doc.Pages.Count; pageNo++)
                    {
                        var page = doc.Pages[pageNo];

                        // SvgExtractor (and the underlying GDI calls) work only on Windows
                        if (!OperatingSystem.IsWindows())
                        {
                            Console.WriteLine($"Skipping SVG extraction for page {pageNo} of '{pdfName}.pdf' (non‑Windows platform).");
                            continue;
                        }

                        // Options: extract every sub‑path to its own SVG file (optional, can be omitted)
                        var options = new SvgExtractionOptions
                        {
                            ExtractEverySubPathToSvg = true,
                            MinStrokeWidth = 0.5
                        };

                        var extractor = new SvgExtractor(options);

                        // Create a sub‑folder for this page's SVG files
                        string pageFolder = Path.Combine(pdfOutputFolder, $"Page_{pageNo}");
                        Directory.CreateDirectory(pageFolder);

                        // Perform the extraction; each sub‑path becomes a separate SVG file inside pageFolder
                        extractor.Extract(page, pageFolder);
                    }
                }

                Console.WriteLine($"Extracted vector graphics from '{pdfName}.pdf' to '{pdfOutputFolder}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfName}.pdf': {ex.Message}");
            }
        }
    }
}
