using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_attachment.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Prepare the attachment content in a memory stream
        byte[] attachmentBytes = System.Text.Encoding.UTF8.GetBytes("This is the content of the attachment.");
        using (MemoryStream attachmentStream = new MemoryStream(attachmentBytes))
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPdf);

            // Create a file specification for the attachment
            var fileSpec = new FileSpecification("Attachment.txt", "Attachment created from a memory stream")
            {
                // Assign the memory stream as the file content
                Contents = attachmentStream
            };

            // Add the file specification to the EmbeddedFiles collection
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the resulting PDF with the embedded attachment
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}
