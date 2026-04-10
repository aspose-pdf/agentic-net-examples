using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDFs
        const string inputDir = "input_pdfs";
        // Output directory for processed PDFs
        const string outputDir = "output_pdfs";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Process each PDF file in the input directory
        foreach (string sourcePath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileName = Path.GetFileName(sourcePath);
            string destPath = Path.Combine(outputDir, fileName);

            try
            {
                // Use PdfContentEditor to modify viewer preferences
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    // Load the PDF file
                    editor.BindPdf(sourcePath);
                    // Set full‑screen mode
                    editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);
                    // Save the modified PDF (overwrites or writes to new location)
                    editor.Save(destPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}