using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "source.pdf";
        const string outputPdf = "with_attachments.pdf";

        // Files to embed: path, MIME type (kept for reference), description
        var attachments = new[]
        {
            new { Path = "image1.png",  Mime = "image/png",          Description = "Company logo" },
            new { Path = "report.docx", Mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document", Description = "Quarterly report" },
            new { Path = "data.csv",    Mime = "text/csv",          Description = "Exported data" }
        };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure all attachment files exist before proceeding
        foreach (var att in attachments)
        {
            if (!File.Exists(att.Path))
            {
                Console.Error.WriteLine($"Attachment file not found: {att.Path}");
                return;
            }
        }

        // Open the source PDF inside a using block (deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Add each file as an embedded file with custom description (MIME type is implicit)
            foreach (var att in attachments)
            {
                // Create a FileSpecification for the attachment using the constructor (file path, description)
                FileSpecification fileSpec = new FileSpecification(att.Path, att.Description)
                {
                    // Set the display name that appears in the attachment list
                    Name = Path.GetFileName(att.Path)
                };
                // Optional: set modification date (helps some viewers)
                fileSpec.Params.ModDate = DateTime.UtcNow;

                // Add to the document's embedded files collection
                doc.EmbeddedFiles.Add(fileSpec);

                // OPTIONAL: create a visible annotation on the first page that points to the attachment
                // Use Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.Rectangle.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);
                FileAttachmentAnnotation annot = new FileAttachmentAnnotation(doc.Pages[1], rect, fileSpec)
                {
                    Title = Path.GetFileNameWithoutExtension(att.Path),
                    Contents = att.Description
                    // No MimeType property on FileSpecification; the MIME type is inferred from the file content.
                };
                doc.Pages[1].Annotations.Add(annot);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with attachments: {outputPdf}");
    }
}
