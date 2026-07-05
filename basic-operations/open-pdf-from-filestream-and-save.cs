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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF from a FileStream and ensure deterministic disposal
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (Document pdfDoc = new Document(inputStream))
        {
            // Save to another file using default PDF settings
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
    }
}