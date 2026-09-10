using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

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

        // Load and parse the JSON file.
        // Expected format:
        // [
        //   {
        //     "File": "doc1.pdf",
        //     "Title": "Document 1",
        //     "Author": "John Doe",
        //     "Subject": "Sample",
        //     "Keywords": "example, test",
        //     "Custom": { "Project": "Alpha", "Version": "1.2" }
        //   },
        //   { ... }
        // ]
        string jsonContent = File.ReadAllText(jsonPath);
        JsonDocument doc = JsonDocument.Parse(jsonContent);
        JsonElement root = doc.RootElement;

        if (root.ValueKind != JsonValueKind.Array)
        {
            Console.Error.WriteLine("Invalid JSON format: root element must be an array.");
            return;
        }

        foreach (JsonElement item in root.EnumerateArray())
        {
            if (!item.TryGetProperty("File", out JsonElement fileElem) ||
                fileElem.GetString() is not string pdfPath ||
                string.IsNullOrWhiteSpace(pdfPath))
            {
                Console.Error.WriteLine("Skipping entry without a valid \"File\" property.");
                continue;
            }

            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF file not found: {pdfPath}");
                continue;
            }

            // Create a PdfFileInfo facade for the target PDF.
            using (PdfFileInfo info = new PdfFileInfo(pdfPath))
            {
                // Apply standard metadata properties if present.
                if (item.TryGetProperty("Title", out JsonElement titleElem))
                    info.Title = titleElem.GetString();

                if (item.TryGetProperty("Author", out JsonElement authorElem))
                    info.Author = authorElem.GetString();

                if (item.TryGetProperty("Subject", out JsonElement subjectElem))
                    info.Subject = subjectElem.GetString();

                if (item.TryGetProperty("Keywords", out JsonElement keywordsElem))
                    info.Keywords = keywordsElem.GetString();

                if (item.TryGetProperty("Creator", out JsonElement creatorElem))
                    info.Creator = creatorElem.GetString();

                // CreationDate and ModDate must be supplied as PDF‑date formatted strings.
                if (item.TryGetProperty("CreationDate", out JsonElement creationDateElem) &&
                    DateTime.TryParse(creationDateElem.GetString(), out DateTime creationDate))
                    info.CreationDate = creationDate.ToString("yyyyMMddHHmmss");

                if (item.TryGetProperty("ModDate", out JsonElement modDateElem) &&
                    DateTime.TryParse(modDateElem.GetString(), out DateTime modDate))
                    info.ModDate = modDate.ToString("yyyyMMddHHmmss");

                // Apply custom metadata (arbitrary name/value pairs).
                if (item.TryGetProperty("Custom", out JsonElement customElem) &&
                    customElem.ValueKind == JsonValueKind.Object)
                {
                    foreach (JsonProperty prop in customElem.EnumerateObject())
                    {
                        string name = prop.Name;
                        string value = prop.Value.GetString() ?? string.Empty;
                        info.SetMetaInfo(name, value);
                    }
                }

                // Save the updated PDF. Overwrite the original file.
                // SaveNewInfo writes the modified metadata without altering the content.
                info.SaveNewInfo(pdfPath);
                Console.WriteLine($"Metadata applied to: {pdfPath}");
            }
        }
    }
}
