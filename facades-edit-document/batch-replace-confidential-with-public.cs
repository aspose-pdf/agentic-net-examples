using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchReplace
{
    static void Main()
    {
        // Folder that contains the PDF files to process
        const string inputFolder = @"C:\PdfArchive";
        // Optional: folder for the processed files (can be the same as inputFolder to overwrite)
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

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Create a PdfContentEditor facade and bind it to the loaded document
                    PdfContentEditor editor = new PdfContentEditor();
                    editor.BindPdf(doc);

                    // Replace every occurrence of "Confidential" with "Public" on all pages
                    // thePage = 0 means "all pages"
                    editor.ReplaceText("Confidential", 0, "Public");

                    // Determine the output file path (overwrite in place or write to output folder)
                    string fileName = Path.GetFileName(pdfPath);
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the modified document
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch replacement completed.");
    }
}