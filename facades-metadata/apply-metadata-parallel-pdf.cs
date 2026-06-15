using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of source PDF files
        var inputFiles = new List<string>
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Directory where updated PDFs will be saved
        string outputDir = "UpdatedPdfs";

        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // New metadata values to apply to each PDF
        string newTitle   = "Updated Title";
        string newAuthor  = "John Doe";
        string newSubject = "Sample Subject";
        string newKeywords = "Aspose, PDF, Metadata";

        // Apply metadata changes in parallel using TPL
        Parallel.ForEach(inputFiles, inputPath =>
        {
            try
            {
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"File not found: {inputPath}");
                    return;
                }

                // Build output file path
                string fileName   = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDir, $"{fileName}_updated.pdf");

                // Load PDF with PdfFileInfo facade
                using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
                {
                    // Set desired metadata properties
                    pdfInfo.Title    = newTitle;
                    pdfInfo.Author   = newAuthor;
                    pdfInfo.Subject  = newSubject;
                    pdfInfo.Keywords = newKeywords;

                    // Save the updated PDF to a new file
                    pdfInfo.SaveNewInfo(outputPath);
                }

                Console.WriteLine($"Metadata updated: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });
    }
}