using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Simple DTO for JSON output
    private class AttachmentInfo
    {
        public string FileName { get; set; }
        public DateTime? CreationDate { get; set; }
    }

    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonOutputPath = "attachments.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        var attachments = new List<AttachmentInfo>();

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all annotations on the page
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // We're interested only in file attachment annotations
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        AttachmentInfo info = new AttachmentInfo
                        {
                            // FileSpecification may be null; guard against it
                            FileName = fileAnn.File?.Name, // <-- corrected property
                            CreationDate = fileAnn.CreationDate
                        };
                        attachments.Add(info);
                    }
                }
            }
        }

        // Serialize the collected information to JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(attachments, jsonOptions);

        // Write JSON to the output file
        File.WriteAllText(jsonOutputPath, json);

        Console.WriteLine($"Attachment metadata written to '{jsonOutputPath}'.");
    }
}
