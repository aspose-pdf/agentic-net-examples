using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of input PDF files to be processed concurrently
        var pdfFiles = new List<string>
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Output directory for the updated PDFs
        string outputDir = "UpdatedPdfs";
        Directory.CreateDirectory(outputDir);

        // Process each PDF in parallel – each thread works with its own PdfFileInfo instance
        Parallel.ForEach(pdfFiles, inputPath =>
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Each thread creates its own PdfFileInfo; no shared mutable state -> thread‑safe
            using (PdfFileInfo info = new PdfFileInfo())
            {
                // Bind the PDF file to the facade
                info.BindPdf(inputPath);

                // Example modifications – set metadata properties
                info.Title = Path.GetFileNameWithoutExtension(inputPath) + " – Updated";
                info.Author = "Automated Process";
                info.Subject = "Thread‑Safe Metadata Update";
                info.Keywords = "Aspose.Pdf;ThreadSafe";

                // Save the updated PDF to a new file
                string outputPath = Path.Combine(outputDir, Path.GetFileName(inputPath));
                info.SaveNewInfo(outputPath);
            }

            Console.WriteLine($"Processed: {inputPath}");
        });

        Console.WriteLine("All PDFs have been processed safely.");
    }
}