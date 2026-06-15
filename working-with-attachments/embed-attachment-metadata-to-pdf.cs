using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";
        const string attachmentPath = "sample.txt";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Ensure the document has at least one page
            doc.Pages.Add();

            // Embed a file attachment if it exists
            if (File.Exists(attachmentPath))
            {
                // Create a FileSpecification for the attachment
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath), "Attachment");
                // Load the file contents into a stream and assign it to the specification
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
                // Add the specification to the EmbeddedFiles collection
                doc.EmbeddedFiles.Add(fileSpec);

                // Add custom metadata about the attachment to the document information dictionary
                doc.Info.Add("AttachmentDescription", "Sample text file attached for reference");
                doc.Info.Add("AttachmentAuthor", "John Doe");
            }

            // Configure PDF save options (no special flags required for metadata)
            PdfSaveOptions saveOptions = new PdfSaveOptions();

            // Save the PDF using the configured options
            doc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with attachment metadata.");
    }
}
