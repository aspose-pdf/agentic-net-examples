using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfPageEditor resides here

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "input_pdfs";
        // Folder where rotated PDFs will be saved
        const string outputFolder = "output_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + "_rotated.pdf");

            try
            {
                // Use PdfPageEditor facade to rotate the first page
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Load the PDF file
                    editor.BindPdf(inputPath);

                    // Set rotation to 90 degrees (valid values: 0, 90, 180, 270)
                    editor.Rotation = 90;

                    // Apply rotation only to the first page (pages are 1‑based)
                    editor.ProcessPages = new int[] { 1 };

                    // Commit the changes
                    editor.ApplyChanges();

                    // Save the modified PDF
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Rotated first page: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{inputPath}': {ex.Message}");
            }
        }
    }
}