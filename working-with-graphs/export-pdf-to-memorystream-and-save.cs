using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a memory stream to hold the PDF data
            using (MemoryStream memory = new MemoryStream())
            {
                // Save the document into the memory stream (PDF format)
                doc.Save(memory);

                // Reset stream position before reading
                memory.Position = 0;

                // Write the memory stream contents to a file on disk
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    memory.CopyTo(file);
                }
            }
        }

        Console.WriteLine($"PDF exported to memory and saved as '{outputPath}'.");
    }
}