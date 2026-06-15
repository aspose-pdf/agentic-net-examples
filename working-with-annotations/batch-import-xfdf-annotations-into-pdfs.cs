using System;
using System.IO;
using Aspose.Pdf;

class BatchXfdfImporter
{
    static void Main()
    {
        // Folder containing PDF files and their matching XFDF files
        const string inputFolder = @"C:\InputFiles";
        // Folder where annotated PDFs will be saved
        const string outputFolder = @"C:\OutputFiles";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Determine matching XFDF file (same file name, .xfdf extension)
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = Path.Combine(inputFolder, baseName + ".xfdf");

            if (!File.Exists(xfdfPath))
            {
                Console.WriteLine($"No XFDF found for '{baseName}'. Skipping.");
                continue;
            }

            // Load the PDF, import annotations from XFDF, and save the result
            try
            {
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Import annotations from the XFDF file; existing content is preserved
                    pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

                    // Save annotated PDF to the output folder (preserve original name)
                    string outputPath = Path.Combine(outputFolder, baseName + "_annotated.pdf");
                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"Annotated PDF saved: {baseName}_annotated.pdf");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{baseName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch import completed.");
    }
}