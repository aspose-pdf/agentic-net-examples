using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        const string inputDirectory = "input_pdfs";
        // Directory where processed PDFs will be saved
        const string outputDirectory = "output_pdfs";

        // Pages to delete (1‑based indexing). Adjust as needed.
        int[] pagesToDelete = new int[] { 2, 3 };

        // Verify input directory exists
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // PdfFileEditor does NOT implement IDisposable, so a plain instance is sufficient
        PdfFileEditor editor = new PdfFileEditor();

        // Process each PDF file in the input directory
        foreach (string inputPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDirectory, $"{fileNameWithoutExt}_trimmed.pdf");

            try
            {
                // Delete the specified pages and save to the output file
                editor.Delete(inputPath, pagesToDelete, outputPath);
                Console.WriteLine($"Deleted pages from '{fileNameWithoutExt}.pdf' -> '{Path.GetFileName(outputPath)}'");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        // No explicit disposal required for PdfFileEditor
    }
}