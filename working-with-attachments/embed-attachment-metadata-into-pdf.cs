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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // OPTIONAL: add an attachment to demonstrate metadata embedding
            const string attachmentPath = "sample.txt";
            if (File.Exists(attachmentPath))
            {
                // Create a file specification for the attachment and add it to the document
                FileSpecification fileSpec = new FileSpecification(attachmentPath);
                doc.EmbeddedFiles.Add(fileSpec);

                // Embed attachment information into the document information dictionary
                // Use the DocumentInfo indexer for custom metadata (AdditionalInfo does not exist).
                doc.Info["AttachmentFileName"] = Path.GetFileName(attachmentPath);
                doc.Info["AttachmentDescription"] = "Sample text attachment";
            }

            // Save the PDF – the custom metadata is stored in the document information dictionary.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with embedded attachment metadata to '{outputPath}'.");
    }
}
