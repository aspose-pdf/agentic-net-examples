using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = "InputPdfs";
        // Folder where XFDF files will be written
        const string outputFolder = "OutputXfdf";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document (wrapped in using for deterministic disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Build the XFDF file name based on the PDF name
                    string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                    string xfdfPath = Path.Combine(outputFolder, baseName + ".xfdf");

                    // Export all annotations from the PDF to the XFDF file
                    doc.ExportAnnotationsToXfdf(xfdfPath);
                }

                Console.WriteLine($"Exported: {Path.GetFileName(pdfPath)} → {Path.GetFileName(outputFolder)}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}