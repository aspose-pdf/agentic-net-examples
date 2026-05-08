using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a memory stream to hold the PDF data
            using (MemoryStream memory = new MemoryStream())
            {
                // Save the document into the memory stream (PDF format by default)
                doc.Save(memory);

                // Reset the stream position before reading its contents
                memory.Position = 0;

                // Write the memory stream to a physical file
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    memory.CopyTo(file);
                }
            }
        }

        Console.WriteLine($"PDF successfully saved to '{outputPath}' via MemoryStream.");
    }
}