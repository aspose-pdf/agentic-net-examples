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
        const string attachmentDescription = "Sample text attachment";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the high‑level Document API
        Document pdf = new Document(inputPath);

        // Prepare the attachment content in a memory stream
        using (MemoryStream attachmentStream = new MemoryStream())
        using (StreamWriter writer = new StreamWriter(attachmentStream))
        {
            writer.Write("This is the content of the attachment.");
            writer.Flush();
            attachmentStream.Position = 0; // reset for reading

            // Create a FileSpecification and add it to the document's EmbeddedFiles collection
            var fileSpec = new FileSpecification(attachmentName)
            {
                Description = attachmentDescription,
                Contents = attachmentStream
            };
            pdf.EmbeddedFiles.Add(fileSpec);
        }

        // Save the modified PDF
        pdf.Save(outputPath);

        // Enumerate embedded files in the PDF
        Console.WriteLine("Embedded files:");
        foreach (FileSpecification spec in pdf.EmbeddedFiles)
        {
            Console.WriteLine($"- {spec.Name}");
        }
    }
}
