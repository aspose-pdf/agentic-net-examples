using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachment = "attachment.txt";
        const string outputPdf = "output_with_metadata.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            // Create a file specification for the attachment.
            var fileSpec = new FileSpecification(attachment, Path.GetFileName(attachment));
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachment));

            // Add the file specification to the document's embedded files collection.
            doc.EmbeddedFiles.Add(fileSpec);

            // Store custom metadata about the attachment in the document information dictionary.
            doc.Info.Add("AttachmentOriginalFileName", Path.GetFileName(attachment));
            doc.Info.Add("AttachmentEmbeddedName", fileSpec.Name);

            // Save the document. Attachments are embedded automatically; no special save option is required.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with attachment metadata: {outputPdf}");
    }
}
