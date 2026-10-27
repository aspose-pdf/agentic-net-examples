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

        // Load the PDF document; Document is disposed automatically.
        using (Document doc = new Document(inputPath))
        {
            // Save the document into a memory stream.
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms); // PDF is written to the stream.
                ms.Position = 0; // Reset stream position before reading.

                // Write the memory stream contents to a physical file.
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(file);
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}' via MemoryStream.");
    }
}