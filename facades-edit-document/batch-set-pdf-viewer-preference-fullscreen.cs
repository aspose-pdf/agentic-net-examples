using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDF files
        const string inputDir = "pdfs";
        // Output directory for PDFs with full‑screen viewer preference
        const string outputDir = "pdfs_fullscreen";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Process each PDF file in the input directory
        foreach (string filePath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            try
            {
                // Initialize the facade and bind the source PDF
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(filePath);

                // Set the viewer preference to full‑screen mode
                editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);

                // Build the output file path (original name with "_full" suffix)
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string outPath = Path.Combine(outputDir, fileName + "_full.pdf");

                // Save the modified PDF
                editor.Save(outPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch viewer‑preference update completed.");
    }
}