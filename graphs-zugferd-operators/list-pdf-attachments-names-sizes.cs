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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Check if there are any embedded files (attachments)
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found.");
                return;
            }

            Console.WriteLine("Attachments:");
            // Iterate over each embedded file and output its name and size
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                string name = fileSpec.Name; // Attachment file name
                long size = 0;
                if (fileSpec.Contents != null)
                {
                    // Ensure the stream is positioned at the beginning
                    if (fileSpec.Contents.CanSeek)
                        fileSpec.Contents.Position = 0;
                    size = fileSpec.Contents.Length; // Size in bytes
                }
                Console.WriteLine($"{name} - {size} bytes");
            }
        }
    }
}
