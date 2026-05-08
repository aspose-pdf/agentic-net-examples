using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string tempPath   = "temp_resized.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Resize page contents (add 10% margins on all sides)
        // ------------------------------------------------------------
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Define resize parameters: 10% left/right/top/bottom margins,
        // content width/height are calculated automatically.
        PdfFileEditor.ContentsResizeParameters resizeParams =
            new PdfFileEditor.ContentsResizeParameters(
                PdfFileEditor.ContentsResizeValue.Percents(10), // left margin
                null,                                          // auto content width
                PdfFileEditor.ContentsResizeValue.Percents(10), // right margin
                PdfFileEditor.ContentsResizeValue.Percents(10), // top margin
                null,                                          // auto content height
                PdfFileEditor.ContentsResizeValue.Percents(10)  // bottom margin
            );

        // Resize all pages (pages argument = null)
        fileEditor.ResizeContents(inputPath, tempPath, null, resizeParams);

        // ------------------------------------------------------------
        // Step 2: Rotate the resized pages by 90 degrees
        // ------------------------------------------------------------
        PdfPageEditor pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(tempPath);

        // Rotate all pages; valid values are 0, 90, 180, 270.
        pageEditor.Rotation = 90;

        // Apply the rotation and save the final document.
        pageEditor.ApplyChanges();
        pageEditor.Save(outputPath);
        pageEditor.Close();

        // Optional cleanup of the intermediate file.
        try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}