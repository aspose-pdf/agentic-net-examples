using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchResizeToA4
{
    static void Main()
    {
        // Source folder containing PDF files
        const string sourceFolder = @"C:\SourcePdfs";
        // Target folder where resized PDFs will be saved
        const string targetFolder = @"C:\ResizedPdfs";

        // Ensure target directory exists
        Directory.CreateDirectory(targetFolder);

        // Verify that the source directory exists before trying to enumerate files
        if (!Directory.Exists(sourceFolder))
        {
            Console.WriteLine($"Source folder does not exist: {sourceFolder}");
            Console.WriteLine("Please create the folder and place PDF files inside before running the program.");
            return;
        }

        // Get all PDF files in the source folder
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in {sourceFolder}.");
            return;
        }

        foreach (string sourcePath in pdfFiles)
        {
            try
            {
                // Determine output file path
                string fileName = Path.GetFileName(sourcePath);
                string outputPath = Path.Combine(targetFolder, fileName);

                // Use PdfPageEditor to change page size to A4
                using (PdfPageEditor pageEditor = new PdfPageEditor())
                {
                    pageEditor.BindPdf(sourcePath);                     // Load the PDF
                    pageEditor.PageSize = PageSize.A4;                  // Set desired page size (A4)
                    pageEditor.ProcessPages = null;                    // Process all pages (null means all)
                    pageEditor.ApplyChanges();                          // Apply the changes
                    pageEditor.Save(outputPath);                        // Save the resized PDF
                }

                Console.WriteLine($"Resized: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process '{sourcePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch resizing completed.");
    }
}
