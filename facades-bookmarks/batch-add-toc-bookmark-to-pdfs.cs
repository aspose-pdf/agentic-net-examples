using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder  = @"C:\PdfFolder";
        // Folder where processed PDFs will be saved (can be the same as inputFolder)
        const string outputFolder = @"C:\PdfFolder\Processed";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_toc.pdf");

            try
            {
                // Initialize the bookmark editor and bind the PDF file
                PdfBookmarkEditor editor = new PdfBookmarkEditor();
                editor.BindPdf(inputPath);

                // Add a top‑level bookmark named "Table of Contents" that points to page 1
                editor.CreateBookmarkOfPage("Table of Contents", 1);

                // Save the modified PDF
                editor.Save(outputPath);

                // Release resources held by the editor
                editor.Close();

                Console.WriteLine($"Processed: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}