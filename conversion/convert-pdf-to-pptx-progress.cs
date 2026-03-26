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

        using (Document doc = new Document(inputPath))
        {
            PptxSaveOptions options = new PptxSaveOptions();
            // Attach custom progress handler
            options.CustomProgressHandler = new PptxSaveOptions.ConversionProgressEventHandler(ShowProgress);
            // Optional: export each slide as an image
            // options.SlidesAsImages = true;

            doc.Save(outputPath, options);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPath}");
    }

    // Progress handler method matching the required delegate signature
    static void ShowProgress(PptxSaveOptions.ProgressEventHandlerInfo info)
    {
        // EventType is an enum; we simply output its string representation
        Console.WriteLine($"{DateTime.Now:T} - {info.EventType}: {info.Value}% (max {info.MaxValue})");
    }
}