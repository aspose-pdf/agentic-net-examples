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
        const string inputFolder = @"C:\PdfFolder";
        // Path for the resulting JSON file
        const string outputJson = @"C:\PdfMetadata\metadata.json";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(outputJson));

        // Collect metadata for each PDF
        var allMetadata = new List<Dictionary<string, object>>();

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Use PdfFileInfo facade to read PDF metadata
            using (Aspose.Pdf.Facades.PdfFileInfo info = new Aspose.Pdf.Facades.PdfFileInfo())
            {
                info.BindPdf(pdfPath);

                var meta = new Dictionary<string, object>
                {
                    ["FileName"]          = Path.GetFileName(pdfPath),
                    ["Title"]             = info.Title,
                    ["Author"]            = info.Author,
                    ["Subject"]           = info.Subject,
                    ["Keywords"]          = info.Keywords,
                    ["Creator"]           = info.Creator,
                    ["Producer"]          = info.Producer,
                    ["CreationDate"]      = info.CreationDate,
                    ["ModDate"]           = info.ModDate,
                    ["NumberOfPages"]     = info.NumberOfPages,
                    ["IsEncrypted"]       = info.IsEncrypted,
                    ["HasOpenPassword"]   = info.HasOpenPassword,
                    ["HasEditPassword"]   = info.HasEditPassword,
                    ["Header"]            = info.Header,
                    ["UseStrictValidation"] = info.UseStrictValidation
                };

                allMetadata.Add(meta);
            }
        }

        // Serialize the collected metadata to JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(allMetadata, jsonOptions);
        File.WriteAllText(outputJson, json);

        Console.WriteLine($"Metadata exported to '{outputJson}'.");
    }
}