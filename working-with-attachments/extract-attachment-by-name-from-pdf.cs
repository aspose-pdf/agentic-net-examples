using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths used in the demo
        const string inputPdfPath = "input.pdf";
        const string attachmentName = "example.txt";
        const string outputDirectory = "ExtractedAttachments";

        // Ensure the target directory exists
        Directory.CreateDirectory(outputDirectory);

        // ---------------------------------------------------------------------
        // 1. Create a sample PDF and embed an attachment so the demo can run.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a blank page (required for a valid PDF)
            seed.Pages.Add();

            // Prepare the attachment content (in a real scenario this could be a file on disk)
            byte[] attachmentContent = System.Text.Encoding.UTF8.GetBytes("This is a sample attachment file.");
            using (MemoryStream attachmentStream = new MemoryStream(attachmentContent))
            {
                // Create a FileSpecification for the attachment
                FileSpecification fileSpec = new FileSpecification(attachmentName, "Sample attachment created inline");
                fileSpec.Contents = attachmentStream;

                // Add the attachment to the PDF
                seed.EmbeddedFiles.Add(fileSpec);
            }

            // Save the PDF that now contains the attachment
            seed.Save(inputPdfPath);
        }

        // ---------------------------------------------------------------------
        // 2. Load the PDF and extract the specified attachment.
        // ---------------------------------------------------------------------
        using (Document doc = new Document(inputPdfPath))
        {
            // Locate the embedded file by its name
            FileSpecification fileSpec = doc.EmbeddedFiles.FindByName(attachmentName);

            if (fileSpec == null)
            {
                Console.Error.WriteLine($"Attachment '{attachmentName}' not found in the PDF.");
                return;
            }

            // Build the full path for the extracted file
            string outputFilePath = Path.Combine(outputDirectory, attachmentName);

            // Save the embedded file to the target location using the Contents stream
            using (FileStream outStream = File.Create(outputFilePath))
            using (Stream contentStream = fileSpec.Contents)
            {
                contentStream.CopyTo(outStream);
            }

            Console.WriteLine($"Attachment '{attachmentName}' extracted to '{outputFilePath}'.");
        }
    }
}
