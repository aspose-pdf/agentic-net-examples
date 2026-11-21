using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentPath = "Invoice2023.pdf";   // file to attach
        const string attachmentDesc = "Invoice2023";

        // Ensure source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF, add the attachment with description and creation date, then save.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the embedded file.
            var fileSpec = new FileSpecification(attachmentPath, attachmentDesc)
            {
                // Embed the file contents.
                Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
            };

            // The FileSpecification class no longer exposes CreationDate/ModDate directly.
            // Use the Params.ModDate property to store the current time (acts as creation date).
            fileSpec.Params.ModDate = DateTime.Now;

            // Add the file to the PDF's EmbeddedFiles collection.
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added to '{outputPdfPath}' with description '{attachmentDesc}'.");
    }
}
