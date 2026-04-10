using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchResizeToA4
{
    static void Main()
    {
        // Source folder containing PDFs to be resized
        const string sourceFolder = @"C:\SourcePdfs";
        // Target folder where resized PDFs will be saved
        const string targetFolder = @"C:\ResizedPdfs";

        // Verify that the source directory exists; if not, inform the user and exit gracefully.
        if (!Directory.Exists(sourceFolder))
        {
            Console.WriteLine($"Source folder '{sourceFolder}' does not exist. No files to process.");
            return;
        }

        // Ensure the target directory exists
        Directory.CreateDirectory(targetFolder);

        // Get all PDF files in the source folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string sourcePath in pdfFiles)
        {
            // Build the output file path preserving the original file name
            string fileName = Path.GetFileName(sourcePath);
            string outputPath = Path.Combine(targetFolder, fileName);

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(sourcePath))
            {
                // Create a PdfPageEditor facade bound to the loaded document
                PdfPageEditor pageEditor = new PdfPageEditor(doc);

                // Set the desired page size – A4 (210mm x 297mm). Aspose.Pdf.PageSize.A4 uses points.
                pageEditor.PageSize = PageSize.A4;

                // No need to set ProcessPages; leaving it unset processes all pages.
                // If you prefer to be explicit, you can assign an empty int array:
                // pageEditor.ProcessPages = new int[] { };

                // Apply the changes (page size adjustment) to the document
                pageEditor.ApplyChanges();

                // Save the modified document to the target location
                doc.Save(outputPath);
            }

            Console.WriteLine($"Resized '{fileName}' to A4 and saved to '{outputPath}'.");
        }

        Console.WriteLine("Batch resizing completed.");
    }
}
