using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Text.Json;

class BatchPdfTextIndexer
{
    // Path to the folder containing PDF files
    private const string InputFolder = @"C:\PdfBatch";

    // Path to the output index file (JSON format)
    private const string IndexFilePath = @"C:\PdfBatch\search_index.json";

    static void Main()
    {
        // Validate input folder
        if (!Directory.Exists(InputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {InputFolder}");
            return;
        }

        // Dictionary to hold file name (or full path) -> extracted text
        var index = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.EnumerateFiles(InputFolder, "*.pdf", SearchOption.TopDirectoryOnly))
        {
            try
            {
                // Load PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Extract all text using TextAbsorber (rule: text-extraction-use-textabsorber-not-page-extracttext)
                    TextAbsorber absorber = new TextAbsorber();
                    // Optional: preserve formatting (pure text mode)
                    absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                    doc.Pages.Accept(absorber);

                    // Store extracted text in the index
                    index[pdfPath] = absorber.Text ?? string.Empty;
                    Console.WriteLine($"Extracted text from: {Path.GetFileName(pdfPath)}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{pdfPath}': {ex.Message}");
            }
        }

        // Serialize the index to JSON for quick retrieval later
        try
        {
            string json = JsonSerializer.Serialize(index, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(IndexFilePath, json);
            Console.WriteLine($"Search index written to: {IndexFilePath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write index file: {ex.Message}");
        }
    }
}