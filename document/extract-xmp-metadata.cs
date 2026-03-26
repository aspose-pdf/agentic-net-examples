using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "metadata.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document pdfDocument = new Document(inputPath))
        {
            using (FileStream xmlStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                pdfDocument.GetXmpMetadata(xmlStream);
            }
        }

        Console.WriteLine($"XMP metadata saved to '{outputPath}'.");
    }
}