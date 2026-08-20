using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes such as PdfPageEditor
using Aspose.Pdf;          // PageSize enum

class BatchResizeToA4
{
    static void Main()
    {
        // Define source and target directories
        const string sourceFolder = @"C:\SourcePdfs";
        const string targetFolder = @"C:\ResizedPdfs";

        // Verify source folder exists
        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        // Create target folder if it does not exist
        Directory.CreateDirectory(targetFolder);

        // Process each PDF file in the source folder
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf");
        foreach (string sourcePath in pdfFiles)
        {
            try
            {
                string fileName = Path.GetFileName(sourcePath);
                string destinationPath = Path.Combine(targetFolder, fileName);

                // PdfPageEditor implements IDisposable via SaveableFacade, so use a using block
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Bind the source PDF file
                    editor.BindPdf(sourcePath);

                    // Set the desired page size (A4)
                    editor.PageSize = PageSize.A4;

                    // Apply the changes to all pages (default behavior)
                    editor.ApplyChanges();

                    // Save the resized PDF to the target location
                    editor.Save(destinationPath);
                }

                Console.WriteLine($"Resized: {fileName} → {destinationPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{sourcePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch resizing completed.");
    }
}