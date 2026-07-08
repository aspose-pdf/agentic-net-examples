using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Directory containing PDF files to process
        const string inputDirectory = @"C:\PdfFiles";

        // Validate directory existence
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Gather all PDF file paths (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        // Thread‑safe collection to store extraction results
        var results = new ConcurrentDictionary<string, string>();

        // Parallel extraction using TPL
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Load the PDF document (lifecycle rule: use Document constructor)
                using (Document doc = new Document(pdfPath))
                {
                    // Create a TextAbsorber (lifecycle rule: use its constructor)
                    TextAbsorber absorber = new TextAbsorber();

                    // Extract text from all pages (correct API usage)
                    doc.Pages.Accept(absorber);

                    // Store the extracted text keyed by file name
                    results[Path.GetFileName(pdfPath)] = absorber.Text;
                }
            }
            catch (Exception ex)
            {
                // Capture any errors per file
                results[Path.GetFileName(pdfPath)] = $"Error: {ex.Message}";
            }
        });

        // Output the results
        foreach (KeyValuePair<string, string> kvp in results)
        {
            Console.WriteLine($"--- {kvp.Key} ---");
            Console.WriteLine(kvp.Value);
            Console.WriteLine();
        }
    }
}