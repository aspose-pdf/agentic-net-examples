using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for PageSize enum

class BatchA4Converter
{
    static void Main()
    {
        // Input folder containing PDFs to be processed
        const string inputFolder = "InputPdfs";
        // Output folder where resized PDFs will be saved
        const string outputFolder = "OutputA4Pdfs";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Create output folder if it does not exist
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            // Build output file path preserving original file name
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // PdfPageEditor is a Facade that can modify page size
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Bind the source PDF file
                    editor.BindPdf(inputPath);

                    // Set the target page size to A4 (210mm x 297mm)
                    editor.PageSize = PageSize.A4;

                    // ProcessPages defaults to all pages when left null,
                    // so we do not need to assign it explicitly.

                    // Save the modified PDF to the output location
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Converted to A4: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}