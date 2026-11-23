using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentName = "sample.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove all existing embedded file attachments
            doc.EmbeddedFiles.Delete();

            // Create a memory stream containing the new attachment data
            byte[] attachmentData = System.Text.Encoding.UTF8.GetBytes("This is a sample attachment.");
            using (MemoryStream stream = new MemoryStream(attachmentData))
            {
                // Build a FileSpecification and assign the stream as its contents
                var fileSpec = new FileSpecification(attachmentName, "Sample attachment");
                fileSpec.Contents = stream;

                // Add the specification to the document's embedded files collection
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new attachment to '{outputPath}'.");
    }
}
