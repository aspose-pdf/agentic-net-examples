using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_attachment.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Sample data for the attachment (could be any binary content)
        byte[] attachmentData = System.Text.Encoding.UTF8.GetBytes("This is the content of the attachment.");

        // Use a MemoryStream so no intermediate file is written to disk
        using (var attachmentStream = new MemoryStream(attachmentData))
        using (var doc = new Document(inputPdf))
        {
            // Create a FileSpecification; the first argument is the name shown in the PDF
            var fileSpec = new FileSpecification("example.txt")
            {
                // Assign the stream containing the file data
                Contents = attachmentStream
            };

            // Add the file specification to the EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}
