using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF from a file stream and ensure proper disposal
        using (FileStream pdfStream = File.OpenRead(pdfPath))
        using (Document doc = new Document(pdfStream))
        {
            // MemoryStream will receive the XMP metadata
            using (MemoryStream xmpStream = new MemoryStream())
            {
                // Extract XMP metadata into the stream
                doc.GetXmpMetadata(xmpStream);

                // Reset the stream position to read from the beginning
                xmpStream.Position = 0;

                // Read the metadata as a UTF‑8 string and output it
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