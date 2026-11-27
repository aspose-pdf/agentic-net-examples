using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "attachment.txt";
        const string outputPdf = "output_with_attachment.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // Load the PDF with the high‑level Document API.
        Document doc = new Document(inputPdf);

        // Create a file specification for the attachment.
        var fileSpec = new FileSpecification(Path.GetFileName(attachmentFile))
        {
            Description = "Sample attachment",
            // Load the file contents.
            Contents = new MemoryStream(File.ReadAllBytes(attachmentFile))
        };

        // Add the attachment to the EmbeddedFiles collection.
        doc.EmbeddedFiles.Add(fileSpec);

        // Save the modified PDF.
        doc.Save(outputPdf);

        // Verify the attachment by enumerating the EmbeddedFiles collection.
        using (Document checkDoc = new Document(outputPdf))
        {
            Console.WriteLine($"Embedded files in '{outputPdf}':");
            foreach (FileSpecification spec in checkDoc.EmbeddedFiles)
            {
                string name = spec.Name;
                string description = spec.Description ?? "(no description)";
                Console.WriteLine($"- {name}: {description}");
            }
        }
    }
}
