using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "attachments";
        const string manifestPath = "manifest.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);
        var manifest = new List<AttachmentInfo>();

        // Load PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                var page = doc.Pages[i];
                foreach (var annotation in page.Annotations)
                {
                    if (annotation is FileAttachmentAnnotation fileAnno)
                    {
                        // Extract attached file using FileSpecification.Name and Contents
                        FileSpecification spec = fileAnno.File;
                        string safeName = Path.GetFileName(spec.Name);
                        string outPath = Path.Combine(outputDir, safeName);

                        // Ensure the stream is at the beginning
                        if (spec.Contents.CanSeek)
                            spec.Contents.Position = 0;

                        using (FileStream outStream = File.Create(outPath))
                        {
                            spec.Contents.CopyTo(outStream);
                        }

                        // Record name and size
                        long size = new FileInfo(outPath).Length;
                        manifest.Add(new AttachmentInfo { Name = safeName, Size = size });
                    }
                }
            }
        }

        // Write manifest JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(manifest, jsonOptions);
        File.WriteAllText(manifestPath, json);
        Console.WriteLine($"Extracted {manifest.Count} attachments. Manifest saved to {manifestPath}");
    }

    private class AttachmentInfo
    {
        // Initialise to avoid CS8618 warnings
        public string Name { get; set; } = string.Empty;
        public long Size { get; set; }
    }
}
