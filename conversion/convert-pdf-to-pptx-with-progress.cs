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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            var pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = false,
                CustomProgressHandler = ProgressHandler
            };

            doc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPath}");
    }

    private static void ProgressHandler(PptxSaveOptions.ProgressEventHandlerInfo info)
    {
        Console.WriteLine($"Progress event: {info.EventType}, Value: {info.Value}/{info.MaxValue}");
    }
}