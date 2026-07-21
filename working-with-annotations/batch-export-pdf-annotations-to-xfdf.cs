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
        const string outputFolder = "XfdfOutputs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Retrieve all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Build the XFDF file name based on the PDF file name *before* the using block
                string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                string xfdfPath = Path.Combine(outputFolder, baseName + ".xfdf");

                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Export all annotations from the document to the XFDF file
                    doc.ExportAnnotationsToXfdf(xfdfPath);
                }

                Console.WriteLine($"Exported annotations: {pdfPath} -> {Path.GetFileName(xfdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
