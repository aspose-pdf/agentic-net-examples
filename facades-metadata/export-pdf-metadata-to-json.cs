using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDF files
        const string folderPath = "pdfs";
        // Output JSON file path
        const string outputJson = "metadata.json";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // List to hold metadata dictionaries for each PDF
        var metadataList = new List<Dictionary<string, object>>();

        // Enumerate all PDF files in the folder
        foreach (string filePath in Directory.EnumerateFiles(folderPath, "*.pdf"))
        {
            // PdfFileInfo reads document metadata without loading the full PDF
            PdfFileInfo info = new PdfFileInfo(filePath);

            var meta = new Dictionary<string, object>
            {
                ["FileName"]          = Path.GetFileName(filePath),
                ["Title"]             = info.Title,
                ["Author"]            = info.Author,
                ["Subject"]           = info.Subject,
                ["Keywords"]          = info.Keywords,
                ["CreationDate"]      = info.CreationDate,
                // Correct property name for modification date in PdfFileInfo
                ["ModificationDate"] = info.ModDate,
                ["Producer"]          = info.Producer,
                ["Creator"]           = info.Creator,
                // PdfFileInfo does not expose a version property; if needed, load the document to get it.
                // ["Version"]        = info.Version // removed – property does not exist
            };

            metadataList.Add(meta);
        }

        // Serialize the list to formatted JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(metadataList, jsonOptions);

        // Write JSON to the output file
        File.WriteAllText(outputJson, json);
        Console.WriteLine($"Metadata exported to '{outputJson}'.");
    }
}
