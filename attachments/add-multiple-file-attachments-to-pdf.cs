using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentPath1 = "attachment1.txt";
        const string attachmentPath2 = "attachment2.jpg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Add first attachment if the file exists
            AddAttachmentIfExists(doc, attachmentPath1);

            // Add second attachment if the file exists
            AddAttachmentIfExists(doc, attachmentPath2);

            // Save the modified PDF (extension is ignored, output is always PDF)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with attachments to '{outputPdf}'.");
    }

    private static void AddAttachmentIfExists(Document doc, string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Attachment not found: {filePath}");
            return;
        }

        // Create a FileSpecification using only the file name (the name that will appear in the PDF)
        var fileSpec = new FileSpecification(Path.GetFileName(filePath))
        {
            Description = $"Embedded file {Path.GetFileName(filePath)}"
        };

        // Load the file bytes into a MemoryStream and assign it to the Contents property
        fileSpec.Contents = new MemoryStream(File.ReadAllBytes(filePath));

        // Add the specification to the document's EmbeddedFiles collection
        doc.EmbeddedFiles.Add(fileSpec);
    }
}