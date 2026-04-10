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

        // Load the PDF document
        Document pdf = new Document(inputPath);

        // Add metadata (Author and Title). If the XMP API is unavailable, the standard PDF Info dictionary is used.
        pdf.Info.Author = "John Doe";
        pdf.Info.Title  = "Project Plan";

        // Save the updated PDF
        pdf.Save(outputPath);

        Console.WriteLine($"Metadata added. Output saved to '{outputPath}'.");
    }
}
