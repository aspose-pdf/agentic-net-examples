using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentsDir = "Attachments";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(attachmentsDir);

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            // The EmbeddedFiles collection contains all file attachments
            foreach (FileSpecification attachment in pdfDocument.EmbeddedFiles)
            {
                // The attachment name as stored in the PDF
                string attachmentName = attachment.Name;

                // Build the full path for saving the attachment
                string outputPath = Path.Combine(attachmentsDir, attachmentName);

                // Reset the stream position if possible
                if (attachment.Contents.CanSeek)
                    attachment.Contents.Position = 0;

                // Write the attachment contents to disk
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    attachment.Contents.CopyTo(outStream);
                }

                Console.WriteLine($"Saved attachment: {outputPath}");
            }
        }

        Console.WriteLine("Attachment extraction completed.");
    }
}
