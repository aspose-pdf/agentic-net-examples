using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing embedded files
        const string inputPdfPath = "input.pdf";

        // Index of the attachment to extract (1‑based indexing)
        const int attachmentIndex = 1;

        // Directory where the extracted file will be saved
        const string outputDirectory = "ExtractedAttachments";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(inputPdfPath))
            {
                // Validate the requested index against the collection count
                if (attachmentIndex < 1 || attachmentIndex > doc.EmbeddedFiles.Count)
                {
                    Console.Error.WriteLine($"Invalid attachment index. Available attachments: {doc.EmbeddedFiles.Count}");
                    return;
                }

                // Retrieve the embedded file specification by index
                FileSpecification fileSpec = doc.EmbeddedFiles[attachmentIndex];

                // Determine the full path for the extracted file (preserve original name)
                string outputFilePath = Path.Combine(outputDirectory, fileSpec.Name);

                // Save the embedded file to disk using its Contents stream
                using (FileStream outStream = File.Create(outputFilePath))
                using (Stream content = fileSpec.Contents)
                {
                    content.CopyTo(outStream);
                }

                Console.WriteLine($"Attachment extracted: {fileSpec.Name}");
                Console.WriteLine($"Saved to: {outputFilePath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting attachment: {ex.Message}");
        }
    }
}
