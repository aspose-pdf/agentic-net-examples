using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Folder containing PDF files to be indexed
        const string inputFolder = "pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // In‑memory searchable index: file path -> extracted text
        var index = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Batch process each PDF in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load PDF (lifecycle rule: use Document constructor)
                using (Document doc = new Document(pdfPath))
                {
                    // Extract all text from the document (text extraction rule)
                    TextAbsorber absorber = new TextAbsorber();
                    doc.Pages.Accept(absorber);

                    // Store extracted text in the index
                    index[pdfPath] = absorber.Text;
                }

                Console.WriteLine($"Indexed: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{pdfPath}': {ex.Message}");
            }
        }

        // Simple interactive search over the in‑memory index
        Console.WriteLine("Enter search term (empty line to exit):");
        string term;
        while (!string.IsNullOrEmpty(term = Console.ReadLine()))
        {
            var matchingFiles = new List<string>();

            foreach (var entry in index)
            {
                if (entry.Value.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    matchingFiles.Add(entry.Key);
                }
            }

            Console.WriteLine($"Found in {matchingFiles.Count} document(s):");
            foreach (string file in matchingFiles)
            {
                Console.WriteLine($" - {file}");
            }

            Console.WriteLine("Enter next search term (empty line to exit):");
        }
    }
}