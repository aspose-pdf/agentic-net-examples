using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_embedded.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            const string attachmentPath = "attachment.txt";
            if (File.Exists(attachmentPath))
            {
                // Create a file specification for the attachment and add it to the PDF
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath));
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
                doc.EmbeddedFiles.Add(fileSpec);

                // Embed attachment metadata into the document information dictionary using the indexer
                doc.Info["AttachmentFileName"] = Path.GetFileName(attachmentPath);
                doc.Info["AttachmentSize"] = new FileInfo(attachmentPath).Length.ToString();
            }

            // Save the PDF – default PdfSaveOptions are sufficient; no EmbedAttachmentMetadata property exists
            var saveOptions = new PdfSaveOptions();
            doc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"Saved PDF with embedded attachment metadata to '{outputPath}'.");
    }
}
