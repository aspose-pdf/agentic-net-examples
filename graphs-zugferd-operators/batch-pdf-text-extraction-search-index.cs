using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;                 // Core PDF API
using Aspose.Pdf.Text;           // TextAbsorber resides here

class Program
{
    // Entry point: expects optional folder path argument containing PDF files.
    static void Main(string[] args)
    {
        // Determine the directory to scan; default to "pdfs" subfolder.
        string inputFolder = args.Length > 0 ? args[0] : "pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Build an in‑memory searchable index: file path → extracted text.
        Dictionary<string, string> index = BuildTextIndex(inputFolder);

        Console.WriteLine($"Indexed {index.Count} PDF document(s).");
        Console.WriteLine("Enter a search term (empty line to exit):");

        while (true)
        {
            string query = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(query))
                break;

            // Perform case‑insensitive search over the indexed texts.
            IEnumerable<string> results = SearchIndex(index, query);

            Console.WriteLine($"Found in {results.Count()} file(s):");
            foreach (string filePath in results)
                Console.WriteLine($"  {filePath}");
        }
    }

    // Scans the specified folder, extracts text from each PDF, and stores it.
    static Dictionary<string, string> BuildTextIndex(string folderPath)
    {
        var index = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Enumerate all PDF files (non‑recursive for simplicity).
        foreach (string pdfFile in Directory.GetFiles(folderPath, "*.pdf"))
        {
            // Ensure deterministic disposal of the Document object.
            using (Document doc = new Document(pdfFile))
            {
                // TextAbsorber extracts all textual content from the document.
                TextAbsorber absorber = new TextAbsorber();

                // Accept the absorber for all pages; this triggers extraction.
                doc.Pages.Accept(absorber);

                // Store the extracted text keyed by the file path.
                index[pdfFile] = absorber.Text ?? string.Empty;
            }
        }

        return index;
    }

    // Returns the file paths whose extracted text contains the query string.
    static IEnumerable<string> SearchIndex(Dictionary<string, string> index, string query)
    {
        // Simple linear scan; suitable for modest data sets.
        foreach (var kvp in index)
        {
            if (kvp.Value != null &&
                kvp.Value.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                yield return kvp.Key;
            }
        }
    }
}