using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_attachments.pdf";

        // List of files to attach
        string[] attachmentFiles = new[]
        {
            "attachment1.docx",
            "attachment2.jpg",
            "missing_file.txt"   // example of a missing file
        };

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                foreach (string filePath in attachmentFiles)
                {
                    if (!File.Exists(filePath))
                    {
                        // Handle missing attachment file – log and skip
                        Console.Error.WriteLine($"Warning: Attachment file not found – '{filePath}'. Skipping.");
                        continue;
                    }

                    // Create a FileSpecification for the attachment.
                    // The file name shown in the PDF is the simple name, the description is optional.
                    var fileSpec = new FileSpecification(Path.GetFileName(filePath))
                    {
                        // Optional description – you can customize as needed
                        Description = $"Embedded attachment: {Path.GetFileName(filePath)}",
                        // Embed the file content via a memory stream
                        Contents = new MemoryStream(File.ReadAllBytes(filePath))
                    };

                    // Add the specification to the EmbeddedFiles collection (the correct API for attachments).
                    pdfDoc.EmbeddedFiles.Add(fileSpec);
                }

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"PDF saved with attachments to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
