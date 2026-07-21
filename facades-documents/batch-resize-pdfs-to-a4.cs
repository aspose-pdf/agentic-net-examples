using System;
using System.IO;
using Aspose.Pdf;               // Added to access PageSize enum
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Source folder containing PDFs to be resized
        const string sourceFolder = "SourcePdfs";
        // Target folder where resized PDFs will be saved
        const string targetFolder = "ResizedPdfs";

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        // Ensure the target directory exists
        Directory.CreateDirectory(targetFolder);

        // Get all PDF files in the source folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string sourcePath in pdfFiles)
        {
            string fileName = Path.GetFileName(sourcePath);
            string destinationPath = Path.Combine(targetFolder, fileName);

            try
            {
                // PdfPageEditor is a facade that handles binding, editing and saving
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Load the source PDF
                    editor.BindPdf(sourcePath);

                    // Set the output page size to A4
                    editor.PageSize = PageSize.A4;

                    // Apply changes to all pages (default behavior)
                    editor.ApplyChanges();

                    // Save the resized PDF to the target location
                    editor.Save(destinationPath);
                }

                Console.WriteLine($"Resized: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch resizing completed.");
    }
}
