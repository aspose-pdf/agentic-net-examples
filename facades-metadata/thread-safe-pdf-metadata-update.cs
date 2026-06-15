using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    // Process a single PDF: read meta info, modify it, and save to a new file.
    static void ProcessPdf(string inputPath, string outputPath, string newTitle)
    {
        // Each thread gets its own PdfFileInfo instance – no shared state.
        using (PdfFileInfo info = new PdfFileInfo())
        {
            // Bind the source PDF file.
            info.BindPdf(inputPath);

            // Modify meta information safely (per‑instance).
            info.Title = newTitle;
            info.Author = "ThreadSafeDemo";
            info.Subject = "Demonstration of thread‑safe PdfFileInfo usage";

            // Save the updated information to a new PDF file.
            // SaveNewInfo writes only the updated metadata without rewriting the whole document.
            info.SaveNewInfo(outputPath);
        }
    }

    static void Main()
    {
        // Example list of input PDFs and corresponding output paths.
        var pdfPairs = new List<(string input, string output, string title)>
        {
            ("doc1.pdf", "doc1_updated.pdf", "Document 1 - Updated"),
            ("doc2.pdf", "doc2_updated.pdf", "Document 2 - Updated"),
            ("doc3.pdf", "doc3_updated.pdf", "Document 3 - Updated")
        };

        // Ensure all input files exist before processing.
        foreach (var (input, _, _) in pdfPairs)
        {
            if (!File.Exists(input))
            {
                Console.Error.WriteLine($"Input file not found: {input}");
                return;
            }
        }

        // Process each PDF in parallel – each task works with its own PdfFileInfo instance.
        Parallel.ForEach(pdfPairs, pair =>
        {
            try
            {
                ProcessPdf(pair.input, pair.output, pair.title);
                Console.WriteLine($"Processed '{pair.input}' -> '{pair.output}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pair.input}': {ex.Message}");
            }
        });

        Console.WriteLine("All PDFs processed.");
    }
}