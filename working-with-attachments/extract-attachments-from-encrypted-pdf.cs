using System;
using System.IO;
using Aspose.Pdf;

class ExtractAttachments
{
    static void Main()
    {
        // Input encrypted PDF path and password
        const string inputPdfPath = "encrypted.pdf";
        const string password = "userPassword";

        // Directory where extracted attachments will be saved
        const string outputDir = "ExtractedAttachments";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Open the encrypted PDF using the password
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath, password))
        {
            // The EmbeddedFiles collection holds all file attachments
            Aspose.Pdf.EmbeddedFileCollection attachments = doc.EmbeddedFiles;

            if (attachments == null || attachments.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            // Iterate over each attachment and save it to the output directory
            foreach (Aspose.Pdf.FileSpecification fileSpec in attachments)
            {
                // Determine a safe file name (use the original name if available)
                string fileName = string.IsNullOrEmpty(fileSpec.Name) ? "attachment.bin" : fileSpec.Name;
                string outputPath = Path.Combine(outputDir, fileName);

                // The Contents property provides a stream with the attachment data
                using (Stream contentStream = fileSpec.Contents)
                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    contentStream.CopyTo(fileStream);
                }

                Console.WriteLine($"Extracted: {fileName} -> {outputPath}");
            }
        }

        Console.WriteLine("Attachment extraction completed.");
    }
}