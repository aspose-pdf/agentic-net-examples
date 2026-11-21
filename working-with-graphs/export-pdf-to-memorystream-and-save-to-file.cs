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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Export the PDF to a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the document into the stream (PDF format by default)
                doc.Save(ms);

                // Ensure the stream position is at the beginning before reading
                ms.Position = 0;

                // Write the stream contents to a file on disk
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(file);
                }
            }
        }

        Console.WriteLine($"PDF exported to memory and saved as '{outputPath}'.");
    }
}