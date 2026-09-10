using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class BatchRotateFirstPage
{
    static void Main()
    {
        // Folder containing PDF files
        const string inputFolder = @"C:\PdfFolder";
        // Output folder for rotated PDFs
        const string outputFolder = @"C:\PdfFolder\Rotated";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            // Build output file name (same name with suffix)
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_rotated.pdf");

            try
            {
                // Use PdfPageEditor facade to rotate pages
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Bind the source PDF file
                    editor.BindPdf(inputPath);

                    // Prepare a dictionary with page‑number → rotation‑degree mapping
                    // Page numbers are 1‑based; rotate only the first page by 90°
                    var rotations = new Dictionary<int, int>
                    {
                        { 1, 90 } // valid values: 0, 90, 180, 270
                    };

                    // Assign the rotation map to the editor
                    editor.PageRotations = rotations;

                    // Apply the changes to the document
                    editor.ApplyChanges();

                    // Save the modified PDF to the output path
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Rotated first page: '{inputPath}' → '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
