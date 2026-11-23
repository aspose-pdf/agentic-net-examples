using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a FileSpecification for the attachment
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
            {
                Description = "Sample attachment",
                UnicodeName = Path.GetFileName(attachmentPath),
                // Embed the file data via a stream
                Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
            };

            // Add the file specification to the EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // EmbeddedFiles collection uses 1‑based indexing
            FileSpecification addedSpec = doc.EmbeddedFiles[1];
            string unicodeName = addedSpec.UnicodeName;
            Console.WriteLine($"UnicodeName of attachment: {unicodeName}");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with attachment saved to '{outputPath}'.");
    }
}