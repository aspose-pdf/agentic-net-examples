using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfBatchIndexer
{
    // Path to the folder containing PDFs to process
    private const string InputFolder = @"C:\PdfInput";

    // Path to the file where the searchable index will be stored (JSON format)
    private const string IndexFilePath = @"C:\PdfIndex\index.json";

    static void Main()
    {
        // Ensure the input folder exists
        if (!Directory.Exists(InputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {InputFolder}");
            return;
        }

        // Prepare a dictionary to hold file name -> extracted text
        var index = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.EnumerateFiles(InputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Use TextAbsorber to extract all text from the document
                    TextAbsorber absorber = new TextAbsorber();

                    // The absorber extracts text from all pages when applied to the Pages collection
                    doc.Pages.Accept(absorber);

                    // Store the extracted text in the index (key = file name without path)
                    string fileName = Path.GetFileName(pdfPath);
                    index[fileName] = absorber.Text ?? string.Empty;
                }

                Console.WriteLine($"Extracted text from: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        // Ensure the directory for the index file exists
        string indexDir = Path.GetDirectoryName(IndexFilePath);
        if (!Directory.Exists(indexDir))
        {
            Directory.CreateDirectory(indexDir);
        }

        // Serialize the index dictionary to JSON and write to disk
        try
        {
            string json = JsonSerializer.Serialize(index, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(IndexFilePath, json);
            Console.WriteLine($"Searchable index saved to: {IndexFilePath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write index file: {ex.Message}");
        }
    }
}