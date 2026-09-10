using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.ofd";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load OFD file with default OFD load options and convert to PDF
        using (Document doc = new Document(inputPath, new OfdLoadOptions()))
        {
            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"OFD converted to PDF: '{outputPath}'");
    }
}