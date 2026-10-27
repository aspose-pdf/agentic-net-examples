using System;
using System.IO;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Directory containing PDF files to process
        const string inputFolder = @"C:\PdfFiles";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Collect all PDF file paths
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        // Thread‑safe collection to store extracted text per file
        var results = new ConcurrentDictionary<string, string>();

        // Parallel extraction using TPL
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Ensure deterministic disposal of the Document
                using (Document doc = new Document(pdfPath))
                {
                    // TextAbsorber extracts text from the whole document
                    TextAbsorber absorber = new TextAbsorber();

                    // Accept the absorber on all pages (pages are 1‑based)
                    doc.Pages.Accept(absorber);

                    // Store the extracted text keyed by file name
                    results[Path.GetFileName(pdfPath)] = absorber.Text;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                results[Path.GetFileName(pdfPath)] = string.Empty;
            }
        });

        // Output results
        foreach (var kvp in results)
        {
            Console.WriteLine($"--- {kvp.Key} ---");
            Console.WriteLine(kvp.Value);
            Console.WriteLine();
        }
    }
}