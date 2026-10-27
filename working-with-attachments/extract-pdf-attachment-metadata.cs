using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;

class AttachmentMetadata
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? CreationDate { get; set; }
    public long? Size { get; set; }
    public string? MimeType { get; set; }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonOutputPath = "attachments.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                var metadataList = new List<AttachmentMetadata>();

                // Iterate over all embedded files (attachments) in the document
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                    {
                        // Size is not a direct property; obtain it from the Contents stream if possible
                        long? size = null;
                        if (fileSpec.Contents != null && fileSpec.Contents.CanSeek)
                        {
                            size = fileSpec.Contents.Length;
                        }

                        var meta = new AttachmentMetadata
                        {
                            Name = fileSpec.Name,
                            Description = fileSpec.Description,
                            // CreationDate is not exposed by the current Aspose.Pdf version; leave as null
                            CreationDate = null,
                            Size = size,
                            MimeType = fileSpec.MIMEType
                        };
                        metadataList.Add(meta);
                    }
                }

                // Serialize the metadata list to JSON with indented formatting
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(metadataList, jsonOptions);

                // Write JSON to the output file
                File.WriteAllText(jsonOutputPath, json);
                Console.WriteLine($"Attachment metadata written to '{jsonOutputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
