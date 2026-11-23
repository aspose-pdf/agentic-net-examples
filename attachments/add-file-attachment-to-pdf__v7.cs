using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentPath = "attachment.txt"; // file to embed
        const string attachmentDescription = "Sample attachment description";

        // Verify files exist
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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment
            // The constructor (string path, string description) loads the file data automatically
            FileSpecification fileSpec = new FileSpecification(attachmentPath, attachmentDescription)
            {
                // Optional: set a description that will appear in PDF viewers
                Description = attachmentDescription,
                // Optional: set modification date
                Params = { ModDate = DateTime.UtcNow }
            };

            // Add the file specification to the document's embedded files collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the updated PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPdfPath}");
    }
}
