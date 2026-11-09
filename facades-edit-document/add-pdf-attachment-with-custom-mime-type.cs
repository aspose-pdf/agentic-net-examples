using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string attachmentPdf = "invoice.pdf";   // file to attach
        const string outputPdf = "output.pdf";        // result PDF

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentPdf))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPdf}");
            return;
        }

        try
        {
            // Load the source PDF
            Document pdfDoc = new Document(inputPdf);

            // Create a FileSpecification for the attachment
            // Constructor takes the display name and a description.
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentPdf), "Invoice Document");
            // Load the file bytes into a stream and assign it to the Contents property.
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPdf));

            // Add the file specification to the EmbeddedFiles collection.
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the result PDF
            pdfDoc.Save(outputPdf);

            Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
