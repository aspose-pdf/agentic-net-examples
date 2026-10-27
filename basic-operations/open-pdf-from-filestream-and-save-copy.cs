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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF as a FileStream.
        using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Load the PDF document from the stream.
            using (Document doc = new Document(stream))
            {
                // Save the document to a new file using default settings.
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}