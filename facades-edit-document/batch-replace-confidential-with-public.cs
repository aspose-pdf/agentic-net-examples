using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchReplace
{
    static void Main()
    {
        // Directory containing the PDF files to process
        const string inputFolder = @"C:\PdfArchive";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Get all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document (lifecycle rule: use using for deterministic disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Create a PdfContentEditor facade and bind the loaded document
                    PdfContentEditor editor = new PdfContentEditor();
                    editor.BindPdf(doc);

                    // Replace every occurrence of "Confidential" with "Public" on all pages
                    editor.ReplaceText("Confidential", "Public");

                    // Save the modified document back to the same file (lifecycle rule: use Document.Save)
                    doc.Save(pdfPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch replacement completed.");
    }
}