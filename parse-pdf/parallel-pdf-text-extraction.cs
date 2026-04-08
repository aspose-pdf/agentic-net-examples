using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Async entry point
    static async Task Main(string[] args)
    {
        // List of PDF files to process
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };

        // Thread‑safe collection for results
        var extractedTexts = new ConcurrentDictionary<string, string>();

        // Create a task for each PDF file
        var tasks = new List<Task>();
        foreach (var pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            tasks.Add(Task.Run(async () =>
            {
                string text = await ExtractTextAsync(pdfPath);
                extractedTexts[pdfPath] = text;
                Console.WriteLine($"Extracted {text.Length} characters from {Path.GetFileName(pdfPath)}");
            }));
        }

        // Wait for all extractions to finish
        await Task.WhenAll(tasks);

        // Optionally write each extracted text to a .txt file
        foreach (var kvp in extractedTexts)
        {
            string txtPath = Path.ChangeExtension(kvp.Key, ".txt");
            await File.WriteAllTextAsync(txtPath, kvp.Value);
        }

        Console.WriteLine("Text extraction completed for all PDFs.");
    }

    // Extracts text from a single PDF using Aspose.Pdf.TextAbsorber
    private static Task<string> ExtractTextAsync(string pdfPath)
    {
        return Task.Run(() =>
        {
            // Load the PDF document (disposed automatically)
            using (Document doc = new Document(pdfPath))
            {
                // Use TextAbsorber to collect text from all pages
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                return absorber.Text ?? string.Empty;
            }
        });
    }
}