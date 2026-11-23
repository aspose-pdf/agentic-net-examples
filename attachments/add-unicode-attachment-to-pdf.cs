using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentPath = "附件文档.txt"; // Unicode file name on disk

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

        using (Document doc = new Document(inputPdfPath))
        {
            // Create a FileSpecification that reads the file and keeps its Unicode name.
            var fileSpec = new FileSpecification(attachmentPath);
            // Optional: set the modification date of the embedded file.
            fileSpec.Params.ModDate = DateTime.UtcNow;

            // Add the specification to the document's embedded files collection.
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the updated PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and PDF saved to '{outputPdfPath}'.");
    }
}
