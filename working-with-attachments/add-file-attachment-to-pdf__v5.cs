using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputFolder = "Output";

        // Validate inputs
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Add a file attachment to the PDF using the EmbeddedFiles collection
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
                {
                    // Load the file bytes into a memory stream and assign to Contents
                    Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
                };
                doc.EmbeddedFiles.Add(fileSpec);

                // Build the full output path
                string outputPdfPath = Path.Combine(outputFolder, "output_with_attachments.pdf");

                // Save the modified PDF
                doc.Save(outputPdfPath);
                Console.WriteLine($"PDF saved with attachments to: {outputPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
