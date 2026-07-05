using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPath = "output_with_attachment.pdf";

        // Verify source files exist
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

        // Load the existing PDF document (recommended lifecycle using "using")
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // Embed a file as an attachment using FileSpecification
            // ------------------------------------------------------------
            var fileSpec = new FileSpecification(attachmentPath, "Attachment file");
            // Load the file contents into the specification
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
            // Add the specification to the document's EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // ------------------------------------------------------------
            // Add custom metadata about the attachment into the document information dictionary
            // ------------------------------------------------------------
            // Document.Info behaves like a dictionary; custom keys are allowed.
            doc.Info.Add("AttachmentDescription", "This PDF contains an attached text file.");

            // ------------------------------------------------------------
            // Configure PDF save options – ensure the document information dictionary
            // (including our custom metadata) is written to the output file.
            // ------------------------------------------------------------
            PdfSaveOptions saveOptions = new PdfSaveOptions();
            // No need to assign DocumentInfo; the Info dictionary is saved automatically.

            // Save the document with the configured options
            doc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF saved with attachment and metadata to '{outputPath}'.");
    }
}
