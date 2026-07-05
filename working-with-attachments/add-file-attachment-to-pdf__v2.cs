using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Path to the file that will be attached to the PDF
        const string attachmentFile = "attachment.txt";

        // Output directory where the new PDF will be saved
        const string outputDirectory = "Output";

        // Validate input files
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

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF, add the attachment, and save to the output folder
        using (Document doc = new Document(inputPdf))
        {
            // Embed the external file using FileSpecification and add it to the document's EmbeddedFiles collection
            using (FileStream attachmentStream = File.OpenRead(attachmentFile))
            {
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentFile), "Attachment");
                fileSpec.Contents = attachmentStream; // set the file data
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Build the full output path
            string outputPath = Path.Combine(
                outputDirectory,
                Path.GetFileNameWithoutExtension(inputPdf) + "_with_attachments.pdf");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF saved with attachments.");
    }
}
