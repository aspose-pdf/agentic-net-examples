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
        {
            // Add a blank page at the end of the document
            doc.Pages.Add();

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page added and saved to '{outputPath}'.");
    }
}