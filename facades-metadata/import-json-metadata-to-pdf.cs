using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace PdfMetadataUpdater
{
    // Represents a single metadata record read from the JSON file.
    public class PdfMetadataRecord
    {
        public string? InputPath { get; set; }          // Path to the source PDF
        public string? OutputPath { get; set; }         // Path where the updated PDF will be saved
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Subject { get; set; }
        public string? Keywords { get; set; }
        public string? Creator { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModDate { get; set; }

        // Any additional custom key/value pairs
        public Dictionary<string, string>? CustomInfo { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string jsonPath = "metadata.json";

            if (!File.Exists(jsonPath))
            {
                Console.Error.WriteLine($"JSON file not found: {jsonPath}");
                return;
            }

            // Read and deserialize the JSON file.
            List<PdfMetadataRecord>? records;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                records = JsonSerializer.Deserialize<List<PdfMetadataRecord>>(jsonContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
                return;
            }

            if (records == null || records.Count == 0)
            {
                Console.WriteLine("No metadata records found in the JSON file.");
                return;
            }

            foreach (var rec in records)
            {
                if (string.IsNullOrWhiteSpace(rec.InputPath) || !File.Exists(rec.InputPath))
                {
                    Console.Error.WriteLine($"Input PDF not found: {rec.InputPath}");
                    continue;
                }

                // Ensure an output path is defined; if not, overwrite the source file.
                string outputPath = string.IsNullOrWhiteSpace(rec.OutputPath) ? rec.InputPath : rec.OutputPath;

                try
                {
                    // Initialize the PdfFileInfo facade for the source PDF.
                    using (PdfFileInfo info = new PdfFileInfo(rec.InputPath))
                    {
                        // Apply standard metadata properties if they are provided.
                        if (!string.IsNullOrEmpty(rec.Title))      info.Title = rec.Title;
                        if (!string.IsNullOrEmpty(rec.Author))     info.Author = rec.Author;
                        if (!string.IsNullOrEmpty(rec.Subject))    info.Subject = rec.Subject;
                        if (!string.IsNullOrEmpty(rec.Keywords))   info.Keywords = rec.Keywords;
                        if (!string.IsNullOrEmpty(rec.Creator))    info.Creator = rec.Creator;

                        // PdfFileInfo expects PDF‑date formatted strings, not DateTime objects.
                        if (rec.CreationDate.HasValue)
                            info.CreationDate = rec.CreationDate.Value.ToString("yyyyMMddHHmmss");
                        if (rec.ModDate.HasValue)
                            info.ModDate = rec.ModDate.Value.ToString("yyyyMMddHHmmss");

                        // Apply any custom metadata entries.
                        if (rec.CustomInfo != null)
                        {
                            foreach (var kvp in rec.CustomInfo)
                            {
                                if (!string.IsNullOrEmpty(kvp.Key))
                                {
                                    info.SetMetaInfo(kvp.Key, kvp.Value ?? string.Empty);
                                }
                            }
                        }

                        // Save the updated PDF to the desired location.
                        info.SaveNewInfo(outputPath);
                    }

                    Console.WriteLine($"Metadata applied and saved to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{rec.InputPath}': {ex.Message}");
                }
            }
        }
    }
}
