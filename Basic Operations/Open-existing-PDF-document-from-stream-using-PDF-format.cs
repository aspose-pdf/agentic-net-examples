using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_copy.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF file as a read‑only stream
        using (FileStream stream = File.OpenRead(inputPath))
        {
            // Load the document from the stream (Document implements IDisposable)
            using (Document doc = new Document(stream))
            {
                // Example operation: save a copy of the PDF to a new file
                doc.Save(outputPath);
                Console.WriteLine($"PDF opened from stream and saved to '{outputPath}'.");
            }
        }
    }
}