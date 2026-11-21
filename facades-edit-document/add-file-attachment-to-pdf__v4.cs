using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPath = "output.pdf";

        // Verify source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Create a FileSpecification for the attachment
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
            {
                Description = Path.GetFileName(attachmentPath),
                // Embed the file data via a stream
                Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
            };

            // Add the file specification to the EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"File attachment added and saved to '{outputPath}'.");
    }
}
