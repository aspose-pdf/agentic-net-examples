using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;                // Text extraction classes

class BatchPdfTextIndexer
{
    static void Main()
    {
        // Directory containing PDF files to process
        const string inputDirectory = @"C:\PdfBatch";
        // Path to the output index file (JSON format)
        const string indexFilePath = @"C:\PdfBatch\searchable_index.json";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDirectory}");
            return;
        }

        // Dictionary to hold file path -> extracted text
        var index = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Process each PDF file in the directory (non‑recursive)
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(pdfPath))
                {
                    // Create a TextAbsorber to extract all text from the document
                    TextAbsorber absorber = new TextAbsorber();

                    // Accept the absorber for all pages
                    doc.Pages.Accept(absorber);

                    // Store the extracted text in the index
                    index[pdfPath] = absorber.Text ?? string.Empty;

                    Console.WriteLine($"Extracted text from: {Path.GetFileName(pdfPath)}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        try
        {
            // Serialize the index to JSON for quick lookup later
            string json = JsonSerializer.Serialize(index, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(indexFilePath, json);
            Console.WriteLine($"Searchable index written to: {indexFilePath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write index file: {ex.Message}");
        }
    }
}