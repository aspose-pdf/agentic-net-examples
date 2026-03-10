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

        // Load the PDF document (lifecycle rule: use Document constructor inside a using block)
        using (Document doc = new Document(inputPath))
        {
            // Create a memory stream that will receive the XMP metadata
            using (MemoryStream xmpStream = new MemoryStream())
            {
                // Extract XMP metadata into the stream (method: Document.GetXmpMetadata)
                doc.GetXmpMetadata(xmpStream);

                // Reset stream position to the beginning for reading
                xmpStream.Position = 0;

                // Read the XML content from the stream
                using (StreamReader reader = new StreamReader(xmpStream))
                {
                    string xmpXml = reader.ReadToEnd();
                    Console.WriteLine("XMP Metadata:");
                    Console.WriteLine(xmpXml);
                }
            }
        }
    }
}