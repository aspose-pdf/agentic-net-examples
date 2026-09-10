using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";               // Source PDF containing attachments
        const string attachmentName = "example.txt";       // Name of the attachment to extract
        const string outputDirectory = "ExtractedFiles";  // Target directory for the saved file

        // Verify source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Ensure the target directory exists
            Directory.CreateDirectory(outputDirectory);

            // Load the PDF document (lifecycle: create, load, save)
            using (Document doc = new Document(pdfPath))
            {
                // Locate the embedded file by its name
                FileSpecification fileSpec = doc.EmbeddedFiles.FindByName(attachmentName);

                if (fileSpec == null)
                {
                    Console.WriteLine($"Attachment '{attachmentName}' not found in the PDF.");
                    return;
                }

                // Build the full path for the extracted file
                string outputPath = Path.Combine(outputDirectory, attachmentName);

                // Save the embedded file to the target location by copying its content stream
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // Reset the position of the content stream just in case
                    if (fileSpec.Contents.CanSeek)
                        fileSpec.Contents.Position = 0;

                    fileSpec.Contents.CopyTo(outStream);
                }

                Console.WriteLine($"Attachment saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
