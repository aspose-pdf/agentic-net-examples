using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to be converted
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Directory where the A4‑sized PDFs will be saved
        string outputDir = "ConvertedA4";
        Directory.CreateDirectory(outputDir);

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Build output file name (original name with _A4 suffix)
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDir, $"{fileName}_A4.pdf");

            // Use PdfPageEditor (Facade) to change page size to A4
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Load the source PDF
                editor.BindPdf(inputPath);

                // Set the desired page size (A4)
                editor.PageSize = PageSize.A4;

                // Apply changes to all pages (default behavior)
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Converted '{inputPath}' to A4 -> '{outputPath}'");
        }
    }
}