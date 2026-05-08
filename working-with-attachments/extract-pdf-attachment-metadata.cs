using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Simple DTO for JSON serialization
    public class AttachmentInfo
    {
        // File name may be null if the annotation does not contain a file specification
        public string? FileName { get; set; }
        public DateTime? CreationDate { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "attachments.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        var attachments = new List<AttachmentInfo>();

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
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
                        AttachmentInfo info = new AttachmentInfo
                        {
                            // Use FileSpecification.Name to get the original file name
                            FileName = fileAnn.File?.Name,
                            CreationDate = fileAnn.CreationDate
                        };
                        attachments.Add(info);
                    }
                }
            }
        }

        // Serialize the list to JSON (pretty printed)
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(attachments, jsonOptions);

        // Write JSON to the output file
        File.WriteAllText(outputJsonPath, json);

        Console.WriteLine($"Attachment metadata written to '{outputJsonPath}'.");
    }
}
