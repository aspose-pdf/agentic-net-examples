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

        // Open the PDF from a FileStream and ensure resources are disposed properly
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (Document pdfDoc = new Document(inputStream))
        {
            // Save the document to a new file using default settings
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
