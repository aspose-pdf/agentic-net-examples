using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_from_stream.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a memory stream to hold the PDF data
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the document to the stream in PDF format
                // SaveFormat.Pdf ensures the output is PDF even when using a stream
                doc.Save(ms, SaveFormat.Pdf);

                // Reset stream position before reading
                ms.Position = 0;

                // Example: write the stream contents to a file
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(file);
                }

                Console.WriteLine($"PDF saved to stream and written to '{outputPath}'.");
            }
        }
    }
}