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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Pages to resize (2 and 4). Page numbers are 1‑based.
        int[] pagesToResize = new int[] { 2, 4 };

        // Define custom margins: left 5% of page width, right 15% of page width.
        // Null values let the API calculate the remaining width/height automatically.
        PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(5),   // left margin
            null,                                            // new contents width (auto)
            PdfFileEditor.ContentsResizeValue.Percents(15), // right margin
            null,                                            // top margin (auto)
            null,                                            // new contents height (auto)
            null                                             // bottom margin (auto)
        );

        PdfFileEditor fileEditor = new PdfFileEditor();
        bool success = fileEditor.ResizeContents(inputPath, outputPath, pagesToResize, parameters);

        if (success)
        {
            Console.WriteLine($"Pages resized and saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Resize operation failed.");
        }
    }
}
