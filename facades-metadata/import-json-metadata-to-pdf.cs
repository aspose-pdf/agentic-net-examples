using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs and matching JSON files (same base name, .json extension)
        const string inputFolder = "InputPdfs";
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string jsonPath = Path.Combine(inputFolder, baseName + ".json");

            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"No metadata JSON for '{pdfPath}'. Skipping.");
                continue;
            }

            // Load metadata from JSON
            Dictionary<string, string> meta = LoadMetadata(jsonPath);

            // Destination path for the updated PDF
            string outputPdfPath = Path.Combine(outputFolder, baseName + ".pdf");

            // Apply metadata using PdfFileInfo facade
            using (PdfFileInfo info = new PdfFileInfo(pdfPath))
            {
                // Standard properties (set only if present in JSON)
                if (meta.TryGetValue("Author", out string author))          info.Author = author;
                if (meta.TryGetValue("Title", out string title))            info.Title = title;
                if (meta.TryGetValue("Subject", out string subject))        info.Subject = subject;
                if (meta.TryGetValue("Keywords", out string keywords))      info.Keywords = keywords;
                if (meta.TryGetValue("Creator", out string creator))        info.Creator = creator;

                // Dates are stored as strings in JSON; attempt to parse to DateTime and convert to PDF‑date string
                if (meta.TryGetValue("CreationDate", out string creationStr) &&
                    DateTime.TryParse(creationStr, out DateTime creationDate))
                {
                    info.CreationDate = creationDate.ToString("yyyyMMddHHmmss"); // PDF date format
                }

                if (meta.TryGetValue("ModDate", out string modStr) &&
                    DateTime.TryParse(modStr, out DateTime modDate))
                {
                    info.ModDate = modDate.ToString("yyyyMMddHHmmss"); // PDF date format
                }

                // Custom metadata (any entry that does not match the standard keys)
                foreach (var kvp in meta)
                {
                    // Skip keys already handled above
                    if (IsStandardKey(kvp.Key)) continue;

                    // Set custom property
                    info.SetMetaInfo(kvp.Key, kvp.Value);
                }

                // Save the PDF with updated metadata
                info.SaveNewInfo(outputPdfPath);
            }

            Console.WriteLine($"Metadata applied and saved: {outputPdfPath}");
        }
    }

    // Loads a simple flat JSON object into a dictionary of string key/value pairs
    private static Dictionary<string, string> LoadMetadata(string jsonFilePath)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using (FileStream fs = File.OpenRead(jsonFilePath))
        {
            using (JsonDocument doc = JsonDocument.Parse(fs))
            {
                if (doc.RootElement.ValueKind == JsonValueKind.Object)
                {
                    foreach (JsonProperty prop in doc.RootElement.EnumerateObject())
                    {
                        // For nested objects, serialize them to JSON string
                        if (prop.Value.ValueKind == JsonValueKind.Object ||
                            prop.Value.ValueKind == JsonValueKind.Array)
                        {
                            dict[prop.Name] = prop.Value.GetRawText();
                        }
                        else
                        {
                            dict[prop.Name] = prop.Value.GetString() ?? string.Empty;
                        }
                    }
                }
            }
        }
        return dict;
    }

    // Determines whether a JSON key corresponds to a standard PdfFileInfo property
    private static bool IsStandardKey(string key)
    {
        return key.Equals("Author", StringComparison.OrdinalIgnoreCase) ||
               key.Equals("Title", StringComparison.OrdinalIgnoreCase) ||
               key.Equals("Subject", StringComparison.OrdinalIgnoreCase) ||
               key.Equals("Keywords", StringComparison.OrdinalIgnoreCase) ||
               key.Equals("Creator", StringComparison.OrdinalIgnoreCase) ||
               key.Equals("CreationDate", StringComparison.OrdinalIgnoreCase) ||
               key.Equals("ModDate", StringComparison.OrdinalIgnoreCase);
    }
}
