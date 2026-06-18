using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfPageEditor resides here

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfFolder";
        // Output folder for rotated PDFs
        const string outputFolder = @"C:\PdfFolder\Rotated";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Build output file name (original name + "_rotated.pdf")
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputFolder, $"{fileName}_rotated.pdf");

                // Use PdfPageEditor facade to rotate the first page
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Bind the source PDF file
                    editor.BindPdf(inputPath);

                    // Specify that only the first page should be processed
                    editor.ProcessPages = new int[] { 1 };

                    // Set rotation to 90 degrees (allowed values: 0,90,180,270)
                    editor.Rotation = 90;

                    // Apply the changes to the document
                    editor.ApplyChanges();

                    // Save the modified PDF to the output path
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Rotated first page: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch rotation completed.");
    }
}