using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades; // PdfFileInfo is in this namespace

class PdfMetadataExporter
{
    // Simple POCO to hold metadata that will be serialized to JSON
    public class MetadataInfo
    {
        // Initialise non‑nullable reference types to avoid CS8618 warnings
        public string FileName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
        // PdfFileInfo exposes dates as strings – keep them as strings to avoid conversion errors
        public string? CreationDate { get; set; }
        public string? ModificationDate { get; set; }
        public string Producer { get; set; } = string.Empty;
        public string Creator { get; set; } = string.Empty;
    }

    static void Main()
    {
        const string folderPath = @"C:\PdfFolder";          // folder containing PDFs
        const string outputJson = @"C:\PdfFolder\metadata.json";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        var metadataList = new List<MetadataInfo>();

        // Iterate over all PDF files in the folder
        foreach (string pdfPath in Directory.GetFiles(folderPath, "*.pdf"))
        {
            try
            {
                // PdfFileInfo extracts document information without loading the full PDF
                var info = new PdfFileInfo(pdfPath);

                var meta = new MetadataInfo
                {
                    FileName = Path.GetFileName(pdfPath),
                    Title = info.Title ?? string.Empty,
                    Author = info.Author ?? string.Empty,
                    Subject = info.Subject ?? string.Empty,
                    Keywords = info.Keywords ?? string.Empty,
                    CreationDate = string.IsNullOrWhiteSpace(info.CreationDate) ? null : info.CreationDate,
                    ModificationDate = string.IsNullOrWhiteSpace(info.ModDate) ? null : info.ModDate,
                    Producer = info.Producer ?? string.Empty,
                    Creator = info.Creator ?? string.Empty
                };

                metadataList.Add(meta);
            }
            catch (Exception ex)
            {
                // Log but continue processing other files
                Console.Error.WriteLine($"Failed to read '{pdfPath}': {ex.Message}");
            }
        }

        try
        {
            // Serialize the list to JSON with indentation for readability
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(metadataList, jsonOptions);
            File.WriteAllText(outputJson, json);
            Console.WriteLine($"Metadata exported to '{outputJson}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing JSON file: {ex.Message}");
        }
    }
}
