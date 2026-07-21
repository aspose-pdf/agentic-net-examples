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
        // Folder containing PDF files to be indexed
        const string inputFolder = "pdfs";
        // Path where the searchable index will be stored (JSON format)
        const string indexPath = "searchIndex.json";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // In‑memory dictionary: key = file name, value = extracted text
        var index = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Iterate over all PDF files in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document (lifecycle rule: use using for deterministic disposal)
                using (Document doc = new Document(pdfFile))
                {
                    // Extract all text from the document
                    TextAbsorber absorber = new TextAbsorber();
                    doc.Pages.Accept(absorber);
                    string extractedText = absorber.Text ?? string.Empty;

                    // Store the result in the index
                    index[Path.GetFileName(pdfFile)] = extractedText;
                }

                Console.WriteLine($"Indexed: {pdfFile}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }

        // Serialize the index to a JSON file for quick retrieval later
        string json = JsonSerializer.Serialize(index, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(indexPath, json);

        Console.WriteLine($"Search index saved to '{indexPath}'.");
    }
}