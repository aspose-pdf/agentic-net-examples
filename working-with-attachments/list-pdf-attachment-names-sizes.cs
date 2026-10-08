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

        // Ensure the Document is disposed properly
        using (Document doc = new Document(inputPath))
        {
            // The Attachments collection does not exist; use EmbeddedFiles instead
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    long sizeInBytes = 0;
                    if (fileSpec.Contents != null)
                    {
                        if (fileSpec.Contents.CanSeek)
                        {
                            sizeInBytes = fileSpec.Contents.Length;
                        }
                        else
                        {
                            // Fallback: copy to a MemoryStream to determine length
                            using (var ms = new MemoryStream())
                            {
                                fileSpec.Contents.CopyTo(ms);
                                sizeInBytes = ms.Length;
                                // Reset the original stream position if possible
                                if (fileSpec.Contents.CanSeek)
                                    fileSpec.Contents.Position = 0;
                            }
                        }
                    }
                    Console.WriteLine($"{fileSpec.Name} - {sizeInBytes} bytes");
                }
            }
            else
            {
                Console.WriteLine("No embedded files found in the PDF.");
            }
        }
    }
}
