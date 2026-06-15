using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the source PDFs
        const string inputDir = "InputPdfs";
        // Directory where the processed PDFs will be saved
        const string outputDir = "OutputPdfs";

        // Pages to delete (1‑based indexing). Adjust as needed.
        int[] pagesToDelete = new int[] { 2, 3 };

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);

        // PdfFileEditor is a facade that works directly with file paths.
        // It does not implement IDisposable, so a simple instance is sufficient.
        PdfFileEditor editor = new PdfFileEditor();

        foreach (string inputPath in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDir, $"{fileNameWithoutExt}_trimmed.pdf");

            try
            {
                // Delete the specified pages and write the result to outputPath.
                // The Delete method returns true on success.
                bool result = editor.Delete(inputPath, pagesToDelete, outputPath);

                if (result)
                {
                    Console.WriteLine($"Successfully processed: {fileNameWithoutExt}.pdf → {Path.GetFileName(outputPath)}");
                }
                else
                {
                    Console.Error.WriteLine($"Deletion failed for: {fileNameWithoutExt}.pdf");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileNameWithoutExt}.pdf: {ex.Message}");
            }
        }
    }
}