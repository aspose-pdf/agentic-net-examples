using System;
using System.IO;
using Aspose.Pdf;               // <-- added for PageSize enum
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

        // Ensure the output directory exists
        Directory.CreateDirectory(targetFolder);

        // Get all PDF files in the source folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string srcPath in pdfFiles)
        {
            string fileName = Path.GetFileName(srcPath);
            string destPath = Path.Combine(targetFolder, fileName);

            // Use PdfPageEditor (Facade) to change the page size to A4
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Load the source PDF
                editor.BindPdf(srcPath);

                // Set the output page size to A4 (Aspose.Pdf.PageSize enum)
                editor.PageSize = PageSize.A4;

                // Apply the changes to all pages (default behavior)
                editor.ApplyChanges();

                // Save the resized PDF to the target location
                editor.Save(destPath);

                // Close the facade (optional, Dispose will also handle it)
                editor.Close();
            }

            Console.WriteLine($"Resized '{fileName}' to A4 and saved to '{destPath}'.");
        }
    }
}
