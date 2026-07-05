using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class PdfMetadataImporter
{
    // Entry point
    static void Main()
    {
        // Paths – adjust as needed
        const string jsonFilePath   = "metadata.json";   // JSON containing metadata
        const string sourcePdfPath  = "source.pdf";      // PDF to update
        const string outputPdfPath  = "updated.pdf";     // Resulting PDF

        if (!File.Exists(jsonFilePath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonFilePath}");
            return;
        }

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {sourcePdfPath}");
            return;
        }

        try
        {
            // Load JSON into a dictionary for easy lookup
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Dictionary<string, JsonElement> metaDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(File.ReadAllText(jsonFilePath), jsonOptions);

            // Open the PDF with PdfFileInfo facade
            using (PdfFileInfo pdfInfo = new PdfFileInfo(sourcePdfPath))
            {
                // Apply known metadata fields if present in JSON
                if (metaDict.TryGetValue("Author", out JsonElement authorElem))
                    pdfInfo.Author = authorElem.GetString();

                if (metaDict.TryGetValue("Title", out JsonElement titleElem))
                    pdfInfo.Title = titleElem.GetString();

                if (metaDict.TryGetValue("Subject", out JsonElement subjectElem))
                    pdfInfo.Subject = subjectElem.GetString();

                if (metaDict.TryGetValue("Keywords", out JsonElement keywordsElem))
                    pdfInfo.Keywords = keywordsElem.GetString();

                if (metaDict.TryGetValue("Creator", out JsonElement creatorElem))
                    pdfInfo.Creator = creatorElem.GetString();

                // CreationDate and ModDate must be supplied as PDF‑date formatted strings (yyyyMMddHHmmss)
                if (metaDict.TryGetValue("CreationDate", out JsonElement creationDateElem) &&
                    DateTime.TryParse(creationDateElem.GetString(), out DateTime creationDate))
                    pdfInfo.CreationDate = creationDate.ToString("yyyyMMddHHmmss");

                if (metaDict.TryGetValue("ModDate", out JsonElement modDateElem) &&
                    DateTime.TryParse(modDateElem.GetString(), out DateTime modDate))
                    pdfInfo.ModDate = modDate.ToString("yyyyMMddHHmmss");

                // Custom metadata can be set via SetMetaInfo(name, value)
                foreach (var kvp in metaDict)
                {
                    string key = kvp.Key;
                    // Skip standard fields already handled
                    if (key.Equals("Author", StringComparison.OrdinalIgnoreCase) ||
                        key.Equals("Title", StringComparison.OrdinalIgnoreCase) ||
                        key.Equals("Subject", StringComparison.OrdinalIgnoreCase) ||
                        key.Equals("Keywords", StringComparison.OrdinalIgnoreCase) ||
                        key.Equals("Creator", StringComparison.OrdinalIgnoreCase) ||
                        key.Equals("CreationDate", StringComparison.OrdinalIgnoreCase) ||
                        key.Equals("ModDate", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    // Store any additional custom property (null values become empty strings)
                    pdfInfo.SetMetaInfo(key, kvp.Value.GetString());
                }

                // Save the PDF with updated metadata
                pdfInfo.SaveNewInfo(outputPdfPath);
            }

            Console.WriteLine($"Metadata applied and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
