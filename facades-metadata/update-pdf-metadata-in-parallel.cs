using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to process
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Directory where updated PDFs will be saved
        string outputDirectory = "Updated";
        Directory.CreateDirectory(outputDirectory);

        // Process each file in parallel
        Parallel.ForEach(inputFiles, inputPath =>
        {
            if (!File.Exists(inputPath))
                return; // Skip missing files

            // Build output file path (adds "_updated" suffix)
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDirectory, $"{fileName}_updated.pdf");

            // Load PDF metadata, modify, and save using PdfFileInfo (Facade API)
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
            {
                // Apply desired metadata changes
                pdfInfo.Title    = "New Title";
                pdfInfo.Author   = "New Author";
                pdfInfo.Subject  = "Updated Subject";
                pdfInfo.Keywords = "keyword1;keyword2";

                // Persist changes to a new PDF file
                pdfInfo.SaveNewInfo(outputPath);
            }
        });

        Console.WriteLine("Metadata updates completed.");
    }
}