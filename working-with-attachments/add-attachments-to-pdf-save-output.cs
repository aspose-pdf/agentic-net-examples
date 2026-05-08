using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Directory where the output PDF will be saved
        const string outputDirectory = "output";

        // Files to be added as attachments to the PDF
        string[] attachmentFiles = { "attach1.txt", "attach2.jpg" };

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Add each existing file as an embedded file (attachment)
            foreach (string filePath in attachmentFiles)
            {
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"Attachment file not found: {filePath}");
                    continue;
                }

                // Create a FileSpecification for the attachment and add it to the document's EmbeddedFiles collection
                FileSpecification attachment = new FileSpecification(filePath);
                pdfDocument.EmbeddedFiles.Add(attachment);
            }

            // Ensure the output directory exists
            Directory.CreateDirectory(outputDirectory);

            // Build the full output file path
            string outputPdfPath = Path.Combine(outputDirectory, "output_with_attachments.pdf");

            // Save the modified PDF to the specified location
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine("PDF saved with attachments.");
    }
}
