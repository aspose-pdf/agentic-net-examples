using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document pdfDoc = new Document(inputPath))
        {
            PptxSaveOptions options = new PptxSaveOptions
            {
                SlidesAsImages = true,
                ImageResolution = 300 // DPI for high‑resolution output
            };

            pdfDoc.Save(outputPath, options);
        }

        Console.WriteLine($"Converted to PPTX: {outputPath}");
    }
}