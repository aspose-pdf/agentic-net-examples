using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Example data to embed as an attachment (in‑memory)
        byte[] attachmentData = System.Text.Encoding.UTF8.GetBytes("This is the content of the attachment.");
        using (MemoryStream attachmentStream = new MemoryStream(attachmentData))
        {
            // Load the PDF
            Document pdf = new Document(sourcePdfPath);

            // Create a FileSpecification for the embedded file
            var fileSpec = new FileSpecification("MyAttachment.txt");
            fileSpec.Contents = attachmentStream; // assign the in‑memory stream

            // Add the file specification to the EmbeddedFiles collection
            pdf.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            pdf.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and PDF saved to '{outputPdfPath}'.");
    }
}
