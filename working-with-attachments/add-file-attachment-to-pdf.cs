using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_attached.pdf";
        const string fileToAttach = "attachment.txt"; // file to embed

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(fileToAttach))
        {
            Console.Error.WriteLine($"Attachment file not found: {fileToAttach}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment.
            // Use the file name as the specification name and provide a description.
            var attachment = new FileSpecification(Path.GetFileName(fileToAttach), "Sample attachment");

            // Set the file data as a stream (required for embedding).
            attachment.Contents = new MemoryStream(File.ReadAllBytes(fileToAttach));

            // Add the attachment to the document's EmbeddedFiles collection.
            pdfDoc.EmbeddedFiles.Add(attachment);

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}
