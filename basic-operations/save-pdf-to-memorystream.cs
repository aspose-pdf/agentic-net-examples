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

        using (Document doc = new Document(inputPath))
        using (MemoryStream ms = new MemoryStream())
        {
            // Save the PDF document into the memory stream
            doc.Save(ms);
            // Ensure the stream position is at the beginning before reading
            ms.Position = 0;
            // Write the stream contents to a file
            using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                ms.CopyTo(file);
            }
        }

        Console.WriteLine($"PDF saved to memory stream and written to '{outputPath}'.");
    }
}