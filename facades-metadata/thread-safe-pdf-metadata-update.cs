using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be processed concurrently
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string outputDirectory = "Modified";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each file in parallel – each thread gets its own PdfFileInfo instance
        Parallel.ForEach(inputFiles, inputPath =>
        {
            if (!File.Exists(inputPath))
                return; // Skip missing files

            // Build output file path
            string baseName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDirectory, $"{baseName}_mod.pdf");

            // PdfFileInfo implements IDisposable, so use a using block for deterministic cleanup
            using (PdfFileInfo pdfInfo = new PdfFileInfo())
            {
                // Bind the source PDF to the facade
                pdfInfo.BindPdf(inputPath);

                // Update meta‑information (each thread works on its own instance)
                pdfInfo.Title    = $"Processed {baseName}";
                pdfInfo.Author   = "ThreadSafe Example";
                pdfInfo.Subject  = "Metadata update";
                pdfInfo.Keywords = "Aspose.Pdf;ThreadSafe";

                // Save the updated PDF to a new file
                pdfInfo.SaveNewInfo(outputPath);
            }

            Console.WriteLine($"Thread {Task.CurrentId} completed: {inputPath} → {outputPath}");
        });
    }
}