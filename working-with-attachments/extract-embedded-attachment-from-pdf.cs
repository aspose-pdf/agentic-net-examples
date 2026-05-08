using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";               // source PDF
        const string attachmentName = "myfile.txt";       // name of the embedded file to extract
        const string outputDirectory = "ExtractedAttachments";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the target directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the PDF document (lifecycle: using → dispose)
            using (Document doc = new Document(pdfPath))
            {
                // Locate the embedded file by its name
                FileSpecification fileSpec = doc.EmbeddedFiles.FindByName(attachmentName);
                if (fileSpec == null || fileSpec.Contents == null)
                {
                    Console.WriteLine($"Attachment '{attachmentName}' not found in the document.");
                    return;
                }

                // Build the full path for the extracted file
                string outputPath = Path.Combine(outputDirectory, attachmentName);

                // Save the embedded file to the target directory using the Contents stream
                using (FileStream outStream = File.Create(outputPath))
                {
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
