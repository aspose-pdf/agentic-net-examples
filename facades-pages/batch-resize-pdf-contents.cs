using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define resize parameters: 10% margins on all sides, content size auto‑calculated
        PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(10), // left margin
            null,                                          // new width (auto)
            PdfFileEditor.ContentsResizeValue.Percents(10), // right margin
            PdfFileEditor.ContentsResizeValue.Percents(10), // top margin
            null,                                          // new height (auto)
            PdfFileEditor.ContentsResizeValue.Percents(10)  // bottom margin
        );

        using (Document document = new Document(inputPath))
        {
            PdfFileEditor fileEditor = new PdfFileEditor();
            fileEditor.ResizeContents(document, parameters);
            document.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
