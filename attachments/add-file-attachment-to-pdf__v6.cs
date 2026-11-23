using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Create a FileSpecification for the attachment, set its contents, and add it to the EmbeddedFiles collection.
            var fileSpec = new FileSpecification(attachmentPath, "Sample attachment description");
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the updated PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with attachment to '{outputPdf}'.");
    }
}
