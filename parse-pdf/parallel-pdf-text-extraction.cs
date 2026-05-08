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
        // Determine the directory that contains PDF files.
        // Use a path relative to the executable so the code works on any OS.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputDirectory = Path.Combine(baseDir, "PdfFiles");

        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"PDF directory not found: {inputDirectory}");
            return;
        }

        // Collect all PDF file paths
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        // Thread‑safe dictionary to hold extraction results: file path -> extracted text
        var results = new ConcurrentDictionary<string, string>();

        // Parallelize the extraction using TPL
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Load the PDF document (wrapped in using for deterministic disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Create a TextAbsorber to extract text from the whole document
                    TextAbsorber absorber = new TextAbsorber();

                    // Accept the absorber for all pages
                    doc.Pages.Accept(absorber);

                    // Store the extracted text
                    results[pdfPath] = absorber.Text;
                }
            }
            catch (Exception ex)
            {
                // In case of errors, store the exception message
                results[pdfPath] = $"Error: {ex.Message}";
            }
        });

        // Output the extraction results
        foreach (KeyValuePair<string, string> kvp in results)
        {
            Console.WriteLine($"--- {Path.GetFileName(kvp.Key)} ---");
            Console.WriteLine(kvp.Value);
            Console.WriteLine();
        }
    }
}
