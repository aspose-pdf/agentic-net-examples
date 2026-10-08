using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace PdfMetadataUpdater
{
    // Represents a single metadata record from the JSON file
    public class MetadataRecord
    {
        public string InputPath { get; set; }      // Path to the source PDF
        public string OutputPath { get; set; }     // Optional: where to save the updated PDF (if null, overwrite InputPath)
        public string Title { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Keywords { get; set; }
        // Additional fields can be added as needed (e.g., Language, Creator, etc.)
    }

    class Program
    {
        static void Main()
        {
            const string jsonFile = "metadata.json";

            if (!File.Exists(jsonFile))
            {
                Console.Error.WriteLine($"Metadata file not found: {jsonFile}");
                return;
            }

            // Read and deserialize the JSON file into a list of metadata records
            List<MetadataRecord> records;
            try
            {
                string jsonContent = File.ReadAllText(jsonFile);
                records = JsonSerializer.Deserialize<List<MetadataRecord>>(jsonContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
                return;
            }

            if (records == null || records.Count == 0)
            {
                Console.WriteLine("No metadata entries found in the JSON file.");
                return;
            }

            foreach (var rec in records)
            {
                // Validate source PDF path
                if (string.IsNullOrWhiteSpace(rec.InputPath) || !File.Exists(rec.InputPath))
                {
                    Console.Error.WriteLine($"Source PDF not found: {rec.InputPath}");
                    continue;
                }

                // Determine the output path (overwrite source if not specified)
                string outputPath = string.IsNullOrWhiteSpace(rec.OutputPath) ? rec.InputPath : rec.OutputPath;

                try
                {
                    // PdfFileInfo works via the Facades API; it does not implement IDisposable,
                    // so no using block is required.
                    PdfFileInfo pdfInfo = new PdfFileInfo();

                    // Bind the existing PDF file
                    pdfInfo.BindPdf(rec.InputPath);

                    // Apply metadata fields if they are provided
                    if (!string.IsNullOrEmpty(rec.Title))    pdfInfo.Title    = rec.Title;
                    if (!string.IsNullOrEmpty(rec.Author))   pdfInfo.Author   = rec.Author;
                    if (!string.IsNullOrEmpty(rec.Subject))  pdfInfo.Subject  = rec.Subject;
                    if (!string.IsNullOrEmpty(rec.Keywords)) pdfInfo.Keywords = rec.Keywords;

                    // Save the updated PDF (overwrites or creates a new file)
                    pdfInfo.Save(outputPath);

                    Console.WriteLine($"Metadata applied to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{rec.InputPath}': {ex.Message}");
                }
            }
        }
    }
}