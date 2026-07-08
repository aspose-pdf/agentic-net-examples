using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchA4Converter
{
    static void Main()
    {
        // Input PDF files (could be populated dynamically)
        string[] inputFiles = new string[]
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Directory where converted PDFs will be saved
        string outputDirectory = "ConvertedA4";
        Directory.CreateDirectory(outputDirectory);

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Build output file name (original name + "_A4.pdf")
            string outputPath = Path.Combine(
                outputDirectory,
                Path.GetFileNameWithoutExtension(inputPath) + "_A4.pdf");

            // Use PdfPageEditor (a SaveableFacade) to change page size to A4
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPath);

                // Set the desired page size (A4)
                editor.PageSize = PageSize.A4;

                // Apply the changes to all pages (default behavior)
                editor.ApplyChanges();

                // Save the modified document
                editor.Save(outputPath);
            }

            Console.WriteLine($"Converted '{inputPath}' to A4 -> '{outputPath}'");
        }
    }
}