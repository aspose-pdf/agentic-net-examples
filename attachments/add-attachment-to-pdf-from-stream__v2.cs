using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_attachment.pdf";
        const string attachmentName = "sample.txt";
        const string attachmentDescription = "Sample attachment description";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a memory stream containing the attachment data
            byte[] attachmentData = System.Text.Encoding.UTF8.GetBytes("This is the content of the attachment.");
            using (MemoryStream ms = new MemoryStream(attachmentData))
            {
                // Create a FileSpecification (the correct type for embedded files), set description, and add it to the document
                var fileSpec = new FileSpecification(ms, attachmentName, attachmentDescription)
                {
                    MIMEType = "text/plain"
                };
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified PDF
            doc.Save(outputPath);

            // Enumerate all embedded files to verify the addition
            Console.WriteLine("Embedded files in the document:");
            foreach (FileSpecification spec in doc.EmbeddedFiles)
            {
                Console.WriteLine($"- Name: {spec.Name}, Description: {spec.Description}");
            }
        }

        Console.WriteLine($"PDF saved with attachment to '{outputPath}'.");
    }
}
