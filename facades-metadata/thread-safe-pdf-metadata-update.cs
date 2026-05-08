using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be processed concurrently
        string[] inputFiles = {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Ensure the output directory exists
        string outputDir = "Processed";
        Directory.CreateDirectory(outputDir);

        // Process each file in parallel – each thread works with its own PdfFileInfo instance
        Parallel.ForEach(inputFiles, inputPath =>
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Derive output file name
            string outputPath = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(inputPath) + "_updated.pdf");

            // Each thread creates, uses, and disposes its own PdfFileInfo instance
            using (PdfFileInfo info = new PdfFileInfo())
            {
                // Bind the PDF file to the facade
                info.BindPdf(inputPath);

                // Modify metadata (example: set Title and Author)
                info.Title = $"Updated {Path.GetFileName(inputPath)}";
                info.Author = "ThreadSafe Processor";

                // Save the updated PDF to a new file
                info.SaveNewInfo(outputPath);
            }

            Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
        });
    }
}