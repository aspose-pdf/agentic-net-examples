using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        var pdfFiles = new List<string>
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Verify that all input files exist before starting parallel work
        foreach (var path in pdfFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"File not found: {path}");
                return;
            }
        }

        // Process each PDF concurrently; each thread works with its own PdfFileInfo instance
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Create a new PdfFileInfo facade for this thread
                using (PdfFileInfo info = new PdfFileInfo())
                {
                    // Bind the PDF file to the facade
                    info.BindPdf(pdfPath);

                    // Update metadata (Title, Author, Subject, Keywords)
                    info.Title = Path.GetFileNameWithoutExtension(pdfPath);
                    info.Author = "ThreadSafeDemo";
                    info.Subject = $"Processed on {DateTime.UtcNow:u}";
                    info.Keywords = "Aspose.Pdf;ThreadSafe";

                    // Save the updated metadata to a new file to avoid conflicts with other threads
                    string outputPath = Path.Combine(
                        Path.GetDirectoryName(pdfPath),
                        Path.GetFileNameWithoutExtension(pdfPath) + "_updated.pdf");

                    // SaveNewInfo writes only the changed metadata while preserving the original content
                    info.SaveNewInfo(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {pdfPath}: {ex.Message}");
            }
        });
    }
}