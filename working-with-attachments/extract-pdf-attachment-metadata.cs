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
        const string inputPdf = "input.pdf";
        const string outputJson = "attachments.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // List to hold attachment info
        var attachmentInfos = new List<object>();

        // Load PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate through annotations on the page
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];

                    // We're interested only in file attachment annotations
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        // Retrieve creation date (may be null)
                        DateTime? creationDate = fileAnn.CreationDate;

                        // Retrieve attached file name if available (use Name property of FileSpecification)
                        string fileName = fileAnn.File?.Name ?? string.Empty;

                        // Build a simple anonymous object for JSON serialization
                        var info = new
                        {
                            PageIndex = i,
                            CreationDate = creationDate?.ToString("o") ?? "",
                            FileName = fileName,
                            Title = fileAnn.Title ?? "",
                            Subject = fileAnn.Subject ?? ""
                        };

                        attachmentInfos.Add(info);
                    }
                }
            }
        }

        // Serialize the list to JSON with indentation for readability
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(attachmentInfos, jsonOptions);

        // Write JSON to the output file
        File.WriteAllText(outputJson, json);

        Console.WriteLine($"Attachment metadata written to '{outputJson}'.");
    }
}
