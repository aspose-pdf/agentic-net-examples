using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve XMP metadata into a memory stream
            using (MemoryStream metaStream = new MemoryStream())
            {
                doc.GetXmpMetadata(metaStream);
                metaStream.Position = 0; // reset for reading

                // Read the metadata as a string (XML)
                using (StreamReader reader = new StreamReader(metaStream))
                {
                    string xmpMetadata = reader.ReadToEnd();
                    Console.WriteLine("XMP Metadata:");
                    Console.WriteLine(xmpMetadata);
                }
            }

            // Save the PDF back (unchanged)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}