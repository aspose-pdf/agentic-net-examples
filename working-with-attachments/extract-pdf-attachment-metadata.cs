using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AttachmentInfo
{
    // Made nullable to satisfy the non‑nullable warnings when the values are not set.
    public string? FileName { get; set; }
    public string? CreationDate { get; set; }
}

class Program
{
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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate through all annotations on the page
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];

                    // We're interested only in FileAttachmentAnnotation objects
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        // Retrieve the associated file specification
                        FileSpecification fileSpec = fileAnn.File;

                        // Build the attachment info object
                        AttachmentInfo info = new AttachmentInfo
                        {
                            FileName = fileSpec?.Name ?? "Unnamed",
                            CreationDate = fileAnn.CreationDate != DateTime.MinValue
                                            ? fileAnn.CreationDate.ToString("o")
                                            : "Unknown"
                        };

                        attachments.Add(info);
                    }
                }
            }
        }

        // Serialize the list to JSON (indented for readability)
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(attachments, jsonOptions);

        // Write JSON to the output file
        File.WriteAllText(outputJsonPath, json);

        Console.WriteLine($"Attachment metadata written to '{outputJsonPath}'.");
    }
}
