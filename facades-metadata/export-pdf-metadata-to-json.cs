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

        var allMetadata = new List<Dictionary<string, object>>();

        // Iterate over each PDF file in the folder
        foreach (string pdfFile in Directory.GetFiles(folderPath, "*.pdf"))
        {
            try
            {
                // Use PdfFileInfo facade to read metadata
                using (PdfFileInfo info = new PdfFileInfo())
                {
                    info.BindPdf(pdfFile);

                    var meta = new Dictionary<string, object>
                    {
                        ["FileName"]        = Path.GetFileName(pdfFile),
                        ["Title"]           = info.Title,
                        ["Author"]          = info.Author,
                        ["Creator"]         = info.Creator,
                        ["Subject"]         = info.Subject,
                        ["Keywords"]        = info.Keywords,
                        ["CreationDate"]    = info.CreationDate,
                        ["ModDate"]         = info.ModDate,
                        ["NumberOfPages"]   = info.NumberOfPages,
                        ["IsEncrypted"]     = info.IsEncrypted,
                        ["IsPdfFile"]       = info.IsPdfFile,
                        ["HasOpenPassword"] = info.HasOpenPassword,
                        ["HasEditPassword"] = info.HasEditPassword,
                        ["Producer"]        = info.Producer,
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

        // Serialize the collected metadata to JSON
        try
        {
            string json = JsonSerializer.Serialize(allMetadata, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJson, json);
            Console.WriteLine($"Metadata exported to '{outputJson}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing JSON file: {ex.Message}");
        }
    }
}