using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Directory containing PDF files to process
        const string inputDirectory = "PdfFiles";
        // Path to the output searchable index (JSON file)
        const string indexFilePath = "searchable_index.json";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Dictionary to hold file name -> extracted text
        var index = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Process each PDF file in the directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                // Open the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Extract text from the whole document
                    TextAbsorber absorber = new TextAbsorber();
                    absorber.Visit(doc); // extracts text from all pages

                    // Store the extracted text in the index
                    string fileName = Path.GetFileName(pdfPath);
                    index[fileName] = absorber.Text ?? string.Empty;

                    Console.WriteLine($"Extracted text from: {fileName}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        // Serialize the index to JSON for quick retrieval later
        try
        {
            string json = JsonSerializer.Serialize(index, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(indexFilePath, json);
            Console.WriteLine($"Searchable index saved to: {indexFilePath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write index file: {ex.Message}");
        }
    }
}