using System;
using System.IO;
using Aspose.Pdf;               // Core PDF types (e.g., PageSize)
using Aspose.Pdf.Facades;      // Facade classes (PdfPageEditor)

class Program
{
    static void Main()
    {
        // List of PDF files to be converted.
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Directory where the A4‑sized PDFs will be saved.
        string outputDir = "ConvertedA4";
        Directory.CreateDirectory(outputDir);

        foreach (string inputPath in inputFiles)
        {
            // Verify the source file exists.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Build the output file name (original name + "_A4.pdf").
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDir, fileName + "_A4.pdf");

            // Use PdfPageEditor (facade) to change the page size.
            // The editor implements IDisposable, so wrap it in a using block.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Load the source PDF.
                editor.BindPdf(inputPath);

                // Set the desired page size – A4.
                editor.PageSize = PageSize.A4;

                // Apply the changes to all pages (default behavior).
                editor.ApplyChanges();

                // Save the modified document to the target path.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Converted '{inputPath}' → '{outputPath}'");
        }
    }
}