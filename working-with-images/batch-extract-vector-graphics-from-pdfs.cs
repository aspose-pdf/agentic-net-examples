using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main(string[] args)
    {
        // Input directory containing PDF files (first argument or default)
        string inputDirectory = args.Length > 0 ? args[0] : "InputPdfs";
        // Output root directory where extracted SVGs will be stored (second argument or default)
        string outputRoot = args.Length > 1 ? args[1] : "ExtractedVectorGraphics";

        // Ensure the output root exists
        Directory.CreateDirectory(outputRoot);

        // Verify that the input directory exists before trying to enumerate files
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input directory '{inputDirectory}' does not exist. No PDFs to process.");
            return;
        }

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Create a dedicated folder for this PDF (named after the file without extension)
            string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputFolder = Path.Combine(outputRoot, pdfName);
            Directory.CreateDirectory(pdfOutputFolder);

            // Load the PDF document inside a using block for deterministic disposal
            using (Document document = new Document(pdfPath))
            {
                // Iterate pages using 1‑based indexing as required by Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= document.Pages.Count; pageIndex++)
                {
                    Page page = document.Pages[pageIndex];

                    // Create a sub‑folder for each page's extracted graphics
                    string pageFolder = Path.Combine(pdfOutputFolder, $"Page_{pageIndex}");
                    Directory.CreateDirectory(pageFolder);

                    // Use SvgExtractor to extract all vector graphics from the page into files
                    SvgExtractor extractor = new SvgExtractor();
                    extractor.Extract(page, pageFolder);
                }
            }

            Console.WriteLine($"Extracted vector graphics from '{pdfPath}' to '{pdfOutputFolder}'.");
        }
    }
}
