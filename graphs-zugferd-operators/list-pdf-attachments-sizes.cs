using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // The EmbeddedFiles collection holds all embedded files (attachments) in the PDF.
            // Use reflection to access Name and Size properties, avoiding a direct dependency on the EmbeddedFile type.
            foreach (var attachment in doc.EmbeddedFiles)
            {
                var type = attachment.GetType();
                var nameProp = type.GetProperty("Name");
                var sizeProp = type.GetProperty("Size");

                string name = nameProp?.GetValue(attachment) as string ?? "<unknown>";
                long size = 0;
                if (sizeProp != null && long.TryParse(sizeProp.GetValue(attachment)?.ToString(), out var parsedSize))
                {
                    size = parsedSize;
                }

                Console.WriteLine($"Attachment: {name}, Size: {size} bytes");
            }
        }
    }
}
