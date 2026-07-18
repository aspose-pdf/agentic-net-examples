using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfTextExtractor
{
    // Asynchronously extracts text from multiple PDF files in parallel.
    // Returns a dictionary where the key is the input file path and the value is the extracted text.
    public static async Task<Dictionary<string, string>> ExtractTextsAsync(string[] pdfFilePaths)
    {
        // Guard clause.
        if (pdfFilePaths == null) throw new ArgumentNullException(nameof(pdfFilePaths));

        // Prepare a thread‑safe collection for the results.
        var results = new Dictionary<string, string>();
        object lockObj = new object();

        // Create a task for each PDF file.
        var extractionTasks = new List<Task>();
        foreach (var path in pdfFilePaths)
        {
            // Skip missing files early.
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"File not found: {path}");
                continue;
            }

            // Launch the extraction on a thread‑pool thread.
            extractionTasks.Add(Task.Run(() =>
            {
                // Load the document inside a using block for deterministic disposal.
                using (Document doc = new Document(path))
                {
                    // Create a TextAbsorber to collect all text.
                    TextAbsorber absorber = new TextAbsorber();

                    // Accept the absorber for all pages of the document.
                    doc.Pages.Accept(absorber);

                    // Store the result in the shared dictionary (protected by a lock).
                    lock (lockObj)
                    {
                        results[path] = absorber.Text;
                    }
                }
            }));
        }

        // Await completion of all extraction tasks.
        await Task.WhenAll(extractionTasks);
        return results;
    }

    // Example usage.
    static async Task Main()
    {
        // Define the PDF files to process.
        string[] pdfFiles = new[]
        {
            "sample1.pdf",
            "sample2.pdf",
            "sample3.pdf"
        };

        // Extract texts in parallel.
        Dictionary<string, string> extractedTexts = await ExtractTextsAsync(pdfFiles);

        // Output the results.
        foreach (var kvp in extractedTexts)
        {
            Console.WriteLine($"--- Text from: {kvp.Key} ---");
            Console.WriteLine(kvp.Value);
            Console.WriteLine();
        }
    }
}