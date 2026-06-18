using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonOutputPath = "attachments.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            var attachmentInfos = new List<Dictionary<string, string>>();

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate through annotations on the page
                foreach (Annotation ann in page.Annotations)
                {
                    // We're interested only in file attachment annotations
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        var info = new Dictionary<string, string>();

                        // File name (if available) – use FileSpecification.Name
                        if (fileAnn.File != null && !string.IsNullOrEmpty(fileAnn.File.Name))
                            info["FileName"] = fileAnn.File.Name;
                        else
                            info["FileName"] = "Unnamed";

                        // Creation date of the annotation – DateTime is not nullable
                        if (fileAnn.CreationDate != DateTime.MinValue)
                            info["CreationDate"] = fileAnn.CreationDate.ToString("o"); // ISO 8601
                        else
                            info["CreationDate"] = "Unknown";

                        // Optional: page index where the attachment resides
                        info["PageIndex"] = i.ToString();

                        attachmentInfos.Add(info);
                    }
                }
            }

            // Serialize the collected metadata to JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(attachmentInfos, jsonOptions);

            // Write JSON to file
            File.WriteAllText(jsonOutputPath, json);
            Console.WriteLine($"Attachment metadata written to '{jsonOutputPath}'.");
        }
    }
}
