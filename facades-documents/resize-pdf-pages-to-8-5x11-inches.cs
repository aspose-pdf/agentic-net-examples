using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // 8.5 x 11 inches expressed in points (1 inch = 72 points)
        double widthPoints = 8.5 * 72;   // 612 points
        double heightPoints = 11 * 72;  // 792 points

        // Create resize parameters that set the new page size
        PdfFileEditor.ContentsResizeParameters resizeParams =
            PdfFileEditor.ContentsResizeParameters.PageResize(widthPoints, heightPoints);

        // Resize all pages using the stream overload of ResizeContents
        using (FileStream src = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream dest = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor editor = new PdfFileEditor();
            // Passing null for pages processes every page in the document
            editor.ResizeContents(src, dest, null, resizeParams);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}