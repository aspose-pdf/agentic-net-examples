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

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // The Attachments collection does not exist – use EmbeddedFiles instead
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found.");
                return;
            }

            // Iterate over all embedded files and display their name and size
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                string name = fileSpec.Name;
                long size = 0;

                if (fileSpec.Contents != null)
                {
                    // Ensure the stream is positioned at the beginning before reading Length
                    if (fileSpec.Contents.CanSeek)
                        fileSpec.Contents.Position = 0;
                    size = fileSpec.Contents.Length;
                }

                Console.WriteLine($"{name} - {size} bytes");
            }
        }
    }
}
