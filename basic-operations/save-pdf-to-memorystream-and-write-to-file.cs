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

        // Load the PDF, save it to a MemoryStream, then write the stream to a file
        using (Document doc = new Document(inputPath))
        using (MemoryStream ms = new MemoryStream())
        {
            // Save PDF content into the memory stream (PDF format by default)
            doc.Save(ms);

            // Reset stream position before reading
            ms.Position = 0;

            // Write the memory stream to the output file
            using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                ms.CopyTo(file);
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}' via MemoryStream.");
    }
}