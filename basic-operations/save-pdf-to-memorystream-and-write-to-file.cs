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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Save the document into a memory stream (PDF format)
            using (MemoryStream memory = new MemoryStream())
            {
                doc.Save(memory); // Document.Save(Stream) writes PDF bytes to the stream

                // Reset stream position before reading
                memory.Position = 0;

                // Write the memory stream contents to a file
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    memory.CopyTo(file);
                }
            }
        }

        Console.WriteLine($"PDF successfully saved to '{outputPath}' via MemoryStream.");
    }
}