using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build the output file path (adds a suffix to avoid overwriting the original)
            string outputPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(inputPath) + "_toc.pdf");

            // Use PdfBookmarkEditor to add a "Table of Contents" bookmark linking to page 1
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                // Bind the source PDF file
                bookmarkEditor.BindPdf(inputPath);

                // Create a top‑level bookmark titled "Table of Contents" that points to page 1
                bookmarkEditor.CreateBookmarkOfPage("Table of Contents", 1);

                // Save the modified PDF to the output location
                bookmarkEditor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
        }
    }
}