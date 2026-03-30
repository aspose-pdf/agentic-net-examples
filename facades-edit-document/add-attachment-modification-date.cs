using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPath = "output.pdf";
        const string description = "Attached file for auditing";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            FileSpecification fileSpec = new FileSpecification(attachmentPath, description);
            // Set modification date to the current UTC timestamp for auditing
            fileSpec.Params.ModDate = DateTime.UtcNow;
            // Add the attachment to the PDF
            doc.EmbeddedFiles.Add(fileSpec);
            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPath}'.");
    }
}
