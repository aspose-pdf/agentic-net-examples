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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Set author and title – these values are written to the XMP metadata automatically.
            doc.Info.Author = "John Doe";
            doc.Info.Title  = "Project Plan";

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"XMP metadata (author, title) added and saved to '{outputPath}'.");
    }
}
