using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = "input_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Iterate over all PDF files in the folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build output file name (original name with "_rotated" suffix)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(sourcePath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(sourcePath) + "_rotated.pdf");

            try
            {
                // Initialize the PdfPageEditor facade and bind the source PDF
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    editor.BindPdf(sourcePath);

                    // Set rotation for the first page (pages are 1‑based)
                    var rotations = new Dictionary<int, int> { { 1, 90 } }; // rotate 90° clockwise
                    editor.PageRotations = rotations;

                    // Apply the rotation and save the result
                    editor.ApplyChanges();
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{sourcePath}': {ex.Message}");
            }
        }
    }
}
