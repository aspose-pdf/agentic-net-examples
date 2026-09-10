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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Save the document into a memory stream (Document.Save(Stream) is the correct overload)
            using (MemoryStream memory = new MemoryStream())
            {
                doc.Save(memory);
                // Reset stream position before reading
                memory.Position = 0;

                // Write the memory stream contents to a physical file
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    memory.CopyTo(file);
                }
            }
        }

        Console.WriteLine($"PDF successfully saved to '{outputPath}' via MemoryStream.");
    }
}