using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing the attachment
        const string pdfPath = "input.pdf";

        // Name of the embedded attachment to extract
        const string attachmentName = "example.txt";

        // Directory where the extracted file will be saved
        const string outputDirectory = "ExtractedAttachments";

        // Validate input PDF existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the target directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the PDF document (using statement ensures proper disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Locate the embedded file by its name
                FileSpecification fileSpec = doc.EmbeddedFiles.FindByName(attachmentName);

                if (fileSpec == null)
                {
                    Console.WriteLine($"Attachment \"{attachmentName}\" not found in the PDF.");
                    return;
                }

                // Determine the full path for the extracted file
                string outputPath = Path.Combine(outputDirectory,
                                                string.IsNullOrEmpty(fileSpec.Name)
                                                    ? attachmentName
                                                    : fileSpec.Name);

                // Save the embedded file to the target location using the Contents stream
                using (Stream source = fileSpec.Contents)
                using (FileStream destination = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    source.CopyTo(destination);
                }

                Console.WriteLine($"Attachment saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting attachment: {ex.Message}");
        }
    }
}
