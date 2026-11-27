using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "attachment.txt";
        const string outputPdf = "output.pdf";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        try
        {
            // Load the PDF using the high‑level Document API (PdfContentEditor does not support file attachments)
            Document pdf = new Document(inputPdf);

            // Create a file specification for the attachment
            var fileSpec = new FileSpecification(attachmentFile)
            {
                Description = "Sample attachment description",
                // Load the attachment content into a memory stream
                Contents = new MemoryStream(File.ReadAllBytes(attachmentFile))
            };

            // Add the attachment to the PDF's EmbeddedFiles collection
            pdf.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            pdf.Save(outputPdf);

            Console.WriteLine($"PDF saved with attachment: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
