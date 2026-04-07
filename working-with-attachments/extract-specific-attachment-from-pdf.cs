using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing attachments
        const string inputPdfPath = "input.pdf";

        // Name of the attachment to extract
        const string attachmentName = "example.txt";

        // Directory where the extracted file will be saved
        const string outputDirectory = "ExtractedAttachments";

        // Ensure the target directory exists
        if (!Directory.Exists(outputDirectory))
            Directory.CreateDirectory(outputDirectory);

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Find the embedded file by its name
                FileSpecification fileSpec = pdfDoc.EmbeddedFiles.FindByName(attachmentName);

                if (fileSpec == null)
                {
                    Console.WriteLine($"Attachment \"{attachmentName}\" not found in the PDF.");
                    return;
                }

                // Build the full path for the extracted file
                string outputFilePath = Path.Combine(outputDirectory, fileSpec.Name);

                // Write the embedded file's contents to disk using the Contents stream
                using (FileStream outStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    // Ensure the stream position is at the beginning
                    if (fileSpec.Contents.CanSeek)
                        fileSpec.Contents.Position = 0;

                    fileSpec.Contents.CopyTo(outStream);
                }

                Console.WriteLine($"Attachment \"{attachmentName}\" extracted to \"{outputFilePath}\".");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
