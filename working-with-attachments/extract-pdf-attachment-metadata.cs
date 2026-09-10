using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string jsonPath = "attachments.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        var attachmentInfos = new List<AttachmentInfo>();

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Annotations collection is also 1‑based
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];

                    // We're interested only in file attachment annotations
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        // FileSpecification uses the Name property for the original file name
                        string fileName = fileAnn.File?.Name;

                        // CreationDate is a non‑nullable DateTime; treat DateTime.MinValue as "not set"
                        string creationDate = null;
                        if (fileAnn.CreationDate != default)
                        {
                            creationDate = fileAnn.CreationDate.ToString("o"); // ISO‑8601 format
                        }

                        AttachmentInfo info = new AttachmentInfo
                        {
                            PageNumber = i,
                            FileName = fileName,
                            CreationDate = creationDate
                        };
                        attachmentInfos.Add(info);
                    }
                }
            }
        }

        // Serialize the collected metadata to JSON (indented for readability)
        string json = JsonSerializer.Serialize(attachmentInfos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonPath, json);
        Console.WriteLine($"Attachment metadata written to {jsonPath}");
    }

    // Simple DTO to hold the relevant metadata for each attachment
    class AttachmentInfo
    {
        public int PageNumber { get; set; }
        public string? FileName { get; set; }
        public string? CreationDate { get; set; }
    }
}
