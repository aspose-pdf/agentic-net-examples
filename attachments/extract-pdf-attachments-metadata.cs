using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "attachments_report.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        var report = new List<AttachmentInfo>();

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // EmbeddedFiles may be null or empty
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                foreach (FileSpecification spec in doc.EmbeddedFiles)
                {
                    // Determine the size of the embedded file by reading its content stream
                    long size = 0;
                    using (var temp = new MemoryStream())
                    {
                        spec.Contents.CopyTo(temp);
                        size = temp.Length;
                    }

                    var info = new AttachmentInfo
                    {
                        Name = spec.Name ?? string.Empty,
                        Size = size
                    };
                    report.Add(info);
                }
            }
        }

        // Serialize the list to formatted JSON
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(report, jsonOptions);
        File.WriteAllText(outputPath, json);

        Console.WriteLine($"Attachment report saved to '{outputPath}'.");
    }

    // DTO used for JSON serialization
    private class AttachmentInfo
    {
        public string Name { get; set; } = string.Empty;
        public long Size { get; set; }
    }
}
