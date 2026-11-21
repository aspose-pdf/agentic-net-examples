using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing attachments
        const string pdfPath = "input.pdf";

        // Name of the attachment to extract (case‑insensitive)
        const string attachmentName = "myfile.txt";

        // Directory where the extracted file will be saved
        const string outputDir = "Attachments";

        // Verify input PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Open the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Locate the attachment (embedded file) by its name
                FileSpecification targetAttachment = null;
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    if (string.Equals(fileSpec.Name, attachmentName, StringComparison.OrdinalIgnoreCase))
                    {
                        targetAttachment = fileSpec;
                        break;
                    }
                }

                if (targetAttachment == null)
                {
                    Console.WriteLine($"Attachment '{attachmentName}' not found in the PDF.");
                    return;
                }

                // Build the full path for the extracted file
                string outputPath = Path.Combine(outputDir, targetAttachment.Name);

                // Save the attachment to the target location using its Contents stream
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    if (targetAttachment.Contents != null)
                    {
                        // Ensure the stream is positioned at the beginning
                        if (targetAttachment.Contents.CanSeek)
                            targetAttachment.Contents.Position = 0;
                        targetAttachment.Contents.CopyTo(outStream);
                    }
                }

                Console.WriteLine($"Attachment saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
