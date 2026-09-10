using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to an existing PDF (could be any source PDF)
        const string inputPdfPath = "input.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Create a memory stream that holds the attachment data (e.g., a text file)
        byte[] attachmentBytes = Encoding.UTF8.GetBytes("This is the content of the in‑memory attachment.");
        using (MemoryStream attachmentStream = new MemoryStream(attachmentBytes))
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Create a FileSpecification from the memory stream.
                // The first argument is the stream, the second is the name that will appear in the PDF attachment list.
                FileSpecification fileSpec = new FileSpecification(attachmentStream, "InMemoryAttachment.txt");

                // Add the file specification to the document's embedded files collection.
                pdfDoc.EmbeddedFiles.Add(fileSpec);

                // Save the modified PDF into another memory stream (no intermediate file on disk).
                using (MemoryStream outputPdfStream = new MemoryStream())
                {
                    pdfDoc.Save(outputPdfStream);

                    // Optionally write the resulting PDF to disk for verification.
                    // The stream position must be reset before reading.
                    outputPdfStream.Position = 0;
                    File.WriteAllBytes("output_with_attachment.pdf", outputPdfStream.ToArray());

                    Console.WriteLine("Attachment added and PDF saved to 'output_with_attachment.pdf'.");
                }
            }
        }
    }
}
