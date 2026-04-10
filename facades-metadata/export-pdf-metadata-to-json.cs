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
        // Output JSON file
        const string outputJson = "metadata.json";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // List to hold metadata dictionaries for each PDF
        var allMetadata = new List<Dictionary<string, object>>();

        // Iterate over all PDF files in the folder
        foreach (string pdfFile in Directory.GetFiles(folderPath, "*.pdf"))
        {
            try
            {
                // Use PdfFileInfo facade to read metadata
                using (PdfFileInfo info = new PdfFileInfo())
                {
                    info.BindPdf(pdfFile); // Load the PDF

                    var meta = new Dictionary<string, object>
                    {
                        ["FileName"]        = Path.GetFileName(pdfFile),
                        ["Title"]           = info.Title,
                        ["Author"]          = info.Author,
                        ["Subject"]         = info.Subject,
                        ["Keywords"]        = info.Keywords,
                        ["Creator"]         = info.Creator,
                        ["Producer"]        = info.Producer,
                        ["CreationDate"]    = info.CreationDate,
                        ["ModDate"]         = info.ModDate,
                        ["NumberOfPages"]   = info.NumberOfPages,
                        ["IsEncrypted"]     = info.IsEncrypted,
                        ["HasOpenPassword"] = info.HasOpenPassword,
                        ["HasEditPassword"] = info.HasEditPassword,
                        ["Header"]          = info.Header
                    };

                    allMetadata.Add(meta);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{pdfFile}': {ex.Message}");
            }
        }

        // Serialize the list to JSON with indentation
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(allMetadata, jsonOptions);

        // Write JSON to the output file
        File.WriteAllText(outputJson, json);
        Console.WriteLine($"Metadata exported to '{outputJson}'.");
    }
}