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

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove any action that would be executed when the document is opened
            doc.OpenAction = null;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Open action removed and saved to '{outputPath}'.");
    }
}