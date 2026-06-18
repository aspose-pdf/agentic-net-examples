using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Source folder containing the original PDFs
        const string sourceFolder = "SourcePdfs";
        // Target folder where resized PDFs will be saved
        const string targetFolder = "ResizedPdfs";

        // Verify source folder exists
        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        // Ensure target folder exists
        Directory.CreateDirectory(targetFolder);

        // Get all PDF files in the source folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string srcPath in pdfFiles)
        {
            string fileName = Path.GetFileName(srcPath);
            string destPath = Path.Combine(targetFolder, fileName);

            // Use PdfPageEditor (facade) to change page size to A4
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Load the source PDF
                editor.BindPdf(srcPath);
                // Set the desired page size (A4)
                editor.PageSize = PageSize.A4;
                // Apply changes to all pages (default ProcessPages = all)
                editor.ApplyChanges();
                // Save the resized PDF to the target location
                editor.Save(destPath);
            }

            Console.WriteLine($"Resized '{fileName}' to A4 and saved to '{destPath}'.");
        }
    }
}