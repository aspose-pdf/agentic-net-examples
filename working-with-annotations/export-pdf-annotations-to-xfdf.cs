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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a memory stream that will receive the XFDF data
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Export all annotations from the document into the stream
                doc.ExportAnnotationsToXfdf(xfdfStream);

                // Reset the stream position so it can be read from the beginning
                xfdfStream.Position = 0;

                // Example: read the XFDF content and write it to the console
                using (StreamReader reader = new StreamReader(xfdfStream))
                {
                    string xfdfContent = reader.ReadToEnd();
                    Console.WriteLine("XFDF data:");
                    Console.WriteLine(xfdfContent);
                }
            }
        }
    }
}