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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the original XMP metadata into a memory stream
            using (MemoryStream xmpStream = new MemoryStream())
            {
                doc.GetXmpMetadata(xmpStream);
                xmpStream.Position = 0;
                using (StreamReader reader = new StreamReader(xmpStream))
                {
                    string xmpXml = reader.ReadToEnd();
                    Console.WriteLine("--- Original XMP Metadata ---");
                    Console.WriteLine(xmpXml);
                }
            }

            // Further processing or modifications can be done here
            // For demonstration, we simply save the document unchanged
            doc.Save("output.pdf");
        }

        Console.WriteLine("Processing completed. Output saved as 'output.pdf'.");
    }
}