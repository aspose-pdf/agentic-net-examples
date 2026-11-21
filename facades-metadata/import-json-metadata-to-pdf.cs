using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Aspose.Pdf.Facades;

class PdfMetadataEntry
{
    [JsonPropertyName("filePath")]
    public string? FilePath { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("author")]
    public string? Author { get; set; }

    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    [JsonPropertyName("keywords")]
    public string? Keywords { get; set; }

    // Additional optional fields can be added here (e.g., Creator, Producer, etc.)
}

class Program
{
    static void Main()
    {
        const string jsonPath = "metadata.json";

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"Metadata file not found: {jsonPath}");
            return;
        }

        // Read and deserialize the JSON file
        PdfMetadataEntry[]? entries;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            entries = JsonSerializer.Deserialize<PdfMetadataEntry[]>(jsonContent);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        if (entries == null || entries.Length == 0)
        {
            Console.WriteLine("No metadata entries found in JSON.");
            return;
        }

        foreach (var entry in entries)
        {
            if (string.IsNullOrWhiteSpace(entry.FilePath))
            {
                Console.Error.WriteLine("Entry missing filePath; skipping.");
                continue;
            }

            if (!File.Exists(entry.FilePath))
            {
                Console.Error.WriteLine($"PDF not found: {entry.FilePath}");
                continue;
            }

            try
            {
                // PdfFileInfo does NOT implement IDisposable; no using block needed
                var info = new PdfFileInfo();

                // Bind the existing PDF
                info.BindPdf(entry.FilePath);

                // Apply metadata if provided
                if (!string.IsNullOrEmpty(entry.Title))   info.Title   = entry.Title;
                if (!string.IsNullOrEmpty(entry.Author))  info.Author  = entry.Author;
                if (!string.IsNullOrEmpty(entry.Subject)) info.Subject = entry.Subject;
                if (!string.IsNullOrEmpty(entry.Keywords))info.Keywords = entry.Keywords;

                // Set creation date – PdfFileInfo expects a string in PDF date format
                // Using the standard PDF date format: D:yyyyMMddHHmmssK
                string pdfNow = DateTime.Now.ToString("'D:'yyyyMMddHHmmssK");
                info.CreationDate = pdfNow;
                // Note: ModificationDate property is not available in the current Aspose.PDF version.
                // If needed, it can be omitted or handled via a different API.

                // Save changes back to the same file (overwrites)
                info.Save(entry.FilePath);

                Console.WriteLine($"Metadata applied to: {entry.FilePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{entry.FilePath}': {ex.Message}");
            }
        }
    }
}
