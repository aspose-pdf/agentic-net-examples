using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // PDF containing attachments
        const string outputRoot   = "ExtractedAttachments"; // Base folder for output

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document (using statement ensures proper disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // The EmbeddedFiles collection holds all file attachments in the PDF
            var attachments = pdfDoc.EmbeddedFiles;

            if (attachments == null || attachments.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            int index = 1;
            foreach (var attachment in attachments) // use object to avoid direct EmbeddedFile reference
            {
                // Create a subfolder for each attachment (e.g., Attachment_1, Attachment_2, ...)
                string subFolder = Path.Combine(outputRoot, $"Attachment_{index}");
                Directory.CreateDirectory(subFolder);

                // ----- Reflection helpers -----
                // Name property (string)
                var nameProp = attachment.GetType().GetProperty("Name");
                string fileName = nameProp?.GetValue(attachment) as string;
                if (string.IsNullOrEmpty(fileName))
                    fileName = $"attachment_{index}";

                // Content property (byte[])
                var contentProp = attachment.GetType().GetProperty("Content");
                byte[] content = contentProp?.GetValue(attachment) as byte[] ?? Array.Empty<byte>();
                // --------------------------------

                string outputFilePath = Path.Combine(subFolder, fileName);

                // Write the attachment to disk
                File.WriteAllBytes(outputFilePath, content);

                Console.WriteLine($"Extracted attachment #{index}: {outputFilePath}");
                index++;
            }
        }

        Console.WriteLine("All attachments have been extracted.");
    }
}
