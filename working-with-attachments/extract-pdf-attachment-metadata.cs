using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    // DTO to hold attachment (embedded file) metadata. Properties are nullable to satisfy non‑nullable warnings.
    private class AttachmentInfo
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreationDate { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "attachments.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            var attachments = new List<AttachmentInfo>();

            // Embedded files are accessed via the Document.EmbeddedFiles collection.
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    // The FileSpecification class no longer exposes a direct CreationDate property.
                    // If the PDF contains a creation date it is stored in the Params dictionary (ModDate is commonly used).
                    // We attempt to read it; if unavailable we keep the value null.
                    DateTime? creationDate = null;
                    try
                    {
                        // Params.ModDate is a DateTime? and is the closest equivalent.
                        creationDate = fileSpec.Params?.ModDate;
                    }
                    catch
                    {
                        // Swallow any unexpected exceptions – the date will remain null.
                    }

                    var info = new AttachmentInfo
                    {
                        Name = fileSpec.Name,
                        Description = fileSpec.Description,
                        CreationDate = creationDate
                    };
                    attachments.Add(info);
                }
            }

            // Serialize the list to JSON with indentation for readability
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(attachments, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Attachment metadata written to '{outputJsonPath}'.");
        }
    }
}
