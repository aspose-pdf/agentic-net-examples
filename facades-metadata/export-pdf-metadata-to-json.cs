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
        // Output JSON file path
        const string outputJson = @"C:\PdfFolder\metadata.json";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        var allMetadata = new List<Dictionary<string, object>>();

        // Iterate over all PDF files in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Use PdfFileInfo facade to read metadata
            using (PdfFileInfo pdfInfo = new PdfFileInfo())
            {
                pdfInfo.BindPdf(pdfPath);

                var meta = new Dictionary<string, object>
                {
                    ["FileName"]       = Path.GetFileName(pdfPath),
                    ["Title"]          = pdfInfo.Title,
                    ["Author"]         = pdfInfo.Author,
                    ["Subject"]        = pdfInfo.Subject,
                    ["Keywords"]       = pdfInfo.Keywords,
                    ["Creator"]        = pdfInfo.Creator,
                    ["Producer"]       = pdfInfo.Producer,
                    ["CreationDate"]   = pdfInfo.CreationDate,
                    ["ModDate"]        = pdfInfo.ModDate,
                    ["NumberOfPages"]  = pdfInfo.NumberOfPages,
                    ["IsEncrypted"]    = pdfInfo.IsEncrypted,
                    ["IsPdfFile"]      = pdfInfo.IsPdfFile,
                    ["HasOpenPassword"]= pdfInfo.HasOpenPassword,
                    ["HasEditPassword"]= pdfInfo.HasEditPassword
                };

                allMetadata.Add(meta);
            }
        }

        // Serialize the list of metadata dictionaries to JSON
        string json = JsonSerializer.Serialize(allMetadata, new JsonSerializerOptions { WriteIndented = true });

        // Write JSON to the output file
        File.WriteAllText(outputJson, json);

        Console.WriteLine($"Metadata exported to '{outputJson}'.");
    }
}