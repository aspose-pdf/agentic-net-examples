using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string sourceFolder = @"C:\PdfInput";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the source folder
        foreach (string inputPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            // Build output file name (original name with "_toc" suffix)
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_toc.pdf");

            // Use PdfBookmarkEditor to add a "Table of Contents" bookmark pointing to page 1
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(inputPath);
            editor.CreateBookmarkOfPage("Table of Contents", 1);
            editor.Save(outputPath);
            editor.Close(); // Release resources
        }

        Console.WriteLine("Batch processing completed.");
    }
}