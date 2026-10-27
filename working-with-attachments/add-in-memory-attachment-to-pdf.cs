using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_attachment.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Prepare attachment data in memory (e.g., a simple text file)
        byte[] attachmentData = System.Text.Encoding.UTF8.GetBytes("This is the attachment content.");

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a FileSpecification for the attachment using a MemoryStream
            var fileSpec = new FileSpecification("sample.txt", "Sample attachment");
            fileSpec.Contents = new MemoryStream(attachmentData);

            // Add the file specification to the EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPath}");
    }
}
