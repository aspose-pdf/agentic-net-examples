using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_8_5x11.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // 1 inch = 72 points. 8.5 x 11 inches => 612 x 792 points
        const double widthPoints  = 8.5 * 72; // 612
        const double heightPoints = 11  * 72; // 792

        // Create resize parameters that set the new page size
        PdfFileEditor.ContentsResizeParameters resizeParams =
            PdfFileEditor.ContentsResizeParameters.PageResize(widthPoints, heightPoints);

        // Use stream overload to apply the new page size to all pages (pages = null)
        using (FileStream srcStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream destStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.ResizeContents(srcStream, destStream, null, resizeParams);
            if (!success)
            {
                Console.Error.WriteLine("Failed to resize page dimensions.");
                return;
            }
        }

        Console.WriteLine($"PDF resized to 8.5x11 inches and saved as '{outputPath}'.");
    }
}