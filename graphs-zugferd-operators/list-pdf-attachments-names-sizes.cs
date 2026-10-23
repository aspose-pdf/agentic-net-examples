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

        using (Document doc = new Document(inputPath))
        {
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found.");
                return;
            }

            Console.WriteLine("Attachments:");
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                string name = fileSpec.Name ?? "Unnamed";
                long size = GetStreamLength(fileSpec.Contents);
                Console.WriteLine($"{name} - {size} bytes");
            }
        }
    }

    private static long GetStreamLength(Stream stream)
    {
        if (stream == null)
            return 0;

        // If the stream supports seeking, use Length directly.
        if (stream.CanSeek)
        {
            long originalPos = stream.Position;
            long length = stream.Length;
            // Reset position to original to avoid side‑effects.
            stream.Position = originalPos;
            return length;
        }

        // Otherwise copy to a MemoryStream to determine length.
        using (var ms = new MemoryStream())
        {
            stream.CopyTo(ms);
            return ms.Length;
        }
    }
}