using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchReplace
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder  = @"C:\PdfArchive";
        // Folder where the modified PDFs will be saved
        const string outputFolder = @"C:\PdfArchive\Processed";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (lifecycle rule: wrap in using)
                using (Document doc = new Document(inputPath))
                {
                    // Create a PdfContentEditor and bind it to the loaded document
                    PdfContentEditor editor = new PdfContentEditor();
                    editor.BindPdf(doc);

                    // Replace all occurrences of "Confidential" with "Public"
                    // thePage = 0 means all pages
                    editor.ReplaceText("Confidential", 0, "Public");

                    // Save the modified document (lifecycle rule: use Save)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch replacement completed.");
    }
}