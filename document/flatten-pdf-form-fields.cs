using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Convert all interactive form fields to static content
            doc.Form.Flatten();

            // Save the result; without SaveOptions this writes a PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form fields flattened and saved to '{outputPath}'.");
    }
}