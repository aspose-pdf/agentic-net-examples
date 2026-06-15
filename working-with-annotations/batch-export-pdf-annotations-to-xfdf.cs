using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder = @"C:\PdfBatch\Input";
        // Folder where the XFDF files will be saved
        const string outputFolder = @"C:\PdfBatch\Xfdf";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Open the PDF document (Document implements IDisposable)
                using (Document doc = new Document(pdfPath))
                {
                    // Build the output XFDF file name – same base name, .xfdf extension
                    string xfdfFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xfdf";
                    string xfdfPath = Path.Combine(outputFolder, xfdfFileName);

                    // Export all annotations of the document to the XFDF file
                    doc.ExportAnnotationsToXfdf(xfdfPath);

                    Console.WriteLine($"Exported annotations from '{Path.GetFileName(pdfPath)}' to '{xfdfFileName}'.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch export completed.");
    }
}