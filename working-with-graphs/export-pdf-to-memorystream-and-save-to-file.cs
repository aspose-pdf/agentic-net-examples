using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_copy.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Export PDF content to a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the document into the memory stream (PDF format)
                doc.Save(ms);

                // Reset stream position before reading
                ms.Position = 0;

                // Write the memory stream to a file on disk
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(file);
                }
            }
        }

        Console.WriteLine($"PDF exported to memory and saved as '{outputPath}'.");
    }
}