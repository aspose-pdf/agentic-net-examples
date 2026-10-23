using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Define files to attach with their MIME types and descriptions
        var attachments = new[]
        {
            new { Path = "file1.txt",  Mime = "text/plain",          Description = "Sample text file" },
            new { Path = "image1.png", Mime = "image/png",           Description = "Sample PNG image" },
            new { Path = "data.json",  Mime = "application/json",    Description = "Sample JSON data" }
        };

        // Verify input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Verify each attachment file exists
        foreach (var att in attachments)
        {
            if (!File.Exists(att.Path))
            {
                Console.Error.WriteLine($"Attachment file not found: {att.Path}");
                return;
            }
        }

        // Load the PDF, attach files, and save
        using (Document doc = new Document(inputPdf))
        {
            foreach (var att in attachments)
            {
                // Create a file specification for the attachment
                var fileSpec = new FileSpecification(Path.GetFileName(att.Path))
                {
                    // Set the file contents as a stream
                    Contents = new MemoryStream(File.ReadAllBytes(att.Path)),
                    // Set custom MIME type and description
                    MIMEType = att.Mime,
                    Description = att.Description
                };

                // Add the attachment to the document's embedded files collection
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachments added and saved to '{outputPdf}'.");
    }
}
