using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            // Rotate the first page 90 degrees clockwise (allowed values: 0, 90, 180, 270)
            Page page = document.Pages[1];
            page.Rotate = Rotation.on90;

            document.Save(outputPath);
        }

        Console.WriteLine("Page rotation applied and saved to 'rotated.pdf'.");
    }
}