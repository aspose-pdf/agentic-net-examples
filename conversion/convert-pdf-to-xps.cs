using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.xps";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document pdfDoc = new Document(inputPath))
        {
            XpsSaveOptions saveOptions = new XpsSaveOptions();
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF converted to XPS: {outputPath}");
    }
}