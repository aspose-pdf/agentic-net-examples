using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing the attachment
        const string pdfPath = "input.pdf";

        // Name of the embedded file to extract
        const string attachmentName = "example.txt";

        // Directory where the extracted file will be saved
        const string targetDirectory = "ExtractedAttachments";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the target directory exists
        Directory.CreateDirectory(targetDirectory);

        try
        {
            // Load the PDF document (using block ensures proper disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Locate the embedded file by its name
                FileSpecification fileSpec = doc.EmbeddedFiles.FindByName(attachmentName);

                if (fileSpec == null)
                {
                    Console.WriteLine($"Attachment \"{attachmentName}\" not found in the PDF.");
                    return;
                }

                // Build the full path for the extracted file
                string outputPath = Path.Combine(targetDirectory, attachmentName);

                // Extract the attachment's content stream and write it to the target file
                using (FileStream output = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // The Contents property returns a Stream with the embedded file data
                    fileSpec.Contents.CopyTo(output);
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
