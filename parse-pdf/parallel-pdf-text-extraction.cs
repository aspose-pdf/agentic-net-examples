using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ParallelPdfTextExtractor
{
    // Entry point
    static async Task Main()
    {
        // Directory containing PDF files
        const string inputDirectory = @"C:\PdfFiles";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Gather all PDF file paths
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        // Dictionary to hold extracted text keyed by file name
        var results = new Dictionary<string, string>();

        // List of tasks, each extracts text from one PDF
        var extractionTasks = new List<Task>();

        foreach (string pdfPath in pdfFiles)
        {
            // Capture the current path for the lambda
            string currentPath = pdfPath;

            // Create a task that extracts text from the PDF
            Task task = Task.Run(() =>
            {
                // Open the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(currentPath))
                {
                    // Create a TextAbsorber to extract all text
                    TextAbsorber absorber = new TextAbsorber();

                    // Accept the absorber for all pages in the document
                    doc.Pages.Accept(absorber);

                    // Store the extracted text in the results dictionary (thread‑safe via lock)
                    lock (results)
                    {
                        results[Path.GetFileName(currentPath)] = absorber.Text;
                    }
                }
            });

            extractionTasks.Add(task);
        }

        // Await completion of all extraction tasks
        await Task.WhenAll(extractionTasks);

        // Output the extracted text for each file
        foreach (var kvp in results)
        {
            Console.WriteLine($"--- Text from {kvp.Key} ---");
            Console.WriteLine(kvp.Value);
            Console.WriteLine();
        }
    }
}