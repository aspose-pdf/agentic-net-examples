using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace PdfMetadataUpdater
{
    // Represents the metadata fields expected in the JSON file.
    public class PdfMetadata
    {
        public string? FilePath { get; set; }          // Path to the source PDF.
        public string? OutputPath { get; set; }        // Optional: where to save the updated PDF.
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Subject { get; set; }
        public string? Keywords { get; set; }
        public string? Creator { get; set; }
        public string? CreationDate { get; set; }      // ISO‑8601 date string.
        public string? ModDate { get; set; }           // ISO‑8601 date string.
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
            string jsonContent = File.ReadAllText(jsonPath);
            List<PdfMetadata> items;
            try
            {
                items = JsonSerializer.Deserialize<List<PdfMetadata>>(jsonContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
                return;
            }

            if (items == null || items.Count == 0)
            {
                Console.WriteLine("No metadata entries found in JSON.");
                return;
            }

            foreach (var meta in items)
            {
                if (string.IsNullOrWhiteSpace(meta.FilePath) || !File.Exists(meta.FilePath))
                {
                    Console.Error.WriteLine($"PDF file not found: {meta.FilePath}");
                    continue;
                }

                // Determine the output path – if not supplied, overwrite the source file.
                string outputPath = string.IsNullOrWhiteSpace(meta.OutputPath)
                    ? meta.FilePath
                    : meta.OutputPath;

                // Use PdfFileInfo facade to modify metadata.
                using (PdfFileInfo pdfInfo = new PdfFileInfo(meta.FilePath))
                {
                    // Apply standard metadata properties if they are provided.
                    if (!string.IsNullOrEmpty(meta.Title))       pdfInfo.Title = meta.Title;
                    if (!string.IsNullOrEmpty(meta.Author))      pdfInfo.Author = meta.Author;
                    if (!string.IsNullOrEmpty(meta.Subject))     pdfInfo.Subject = meta.Subject;
                    if (!string.IsNullOrEmpty(meta.Keywords))    pdfInfo.Keywords = meta.Keywords;
                    if (!string.IsNullOrEmpty(meta.Creator))     pdfInfo.Creator = meta.Creator;

                    // Parse and assign dates when valid – use PDF‑date string format.
                    if (DateTime.TryParse(meta.CreationDate, out DateTime creation))
                        pdfInfo.CreationDate = creation.ToString("yyyyMMddHHmmss");

                    if (DateTime.TryParse(meta.ModDate, out DateTime mod))
                        pdfInfo.ModDate = mod.ToString("yyyyMMddHHmmss");

                    // Save the updated metadata to the desired file.
                    pdfInfo.SaveNewInfo(outputPath);
                }

                Console.WriteLine($"Metadata applied to: {outputPath}");
            }
        }
    }
}
