using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create an instance of PdfFileEditor (no IDisposable, so no using block).
        PdfFileEditor editor = new PdfFileEditor();

        // Define asymmetric margins:
        // Left 5% of page width, Right 15% of page width,
        // Top 10% of page height, Bottom 10% of page height.
        // Width and height of the content are left null for automatic calculation.
        PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(5),   // left margin
            null,                                            // contents width (auto)
            PdfFileEditor.ContentsResizeValue.Percents(15),  // right margin
            PdfFileEditor.ContentsResizeValue.Percents(10), // top margin
            null,                                            // contents height (auto)
            PdfFileEditor.ContentsResizeValue.Percents(10)  // bottom margin
        );

        // Process all pages (null array means all pages).
        using (FileStream src = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream dest = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            bool success = editor.ResizeContents(src, dest, null, parameters);
            if (!success)
            {
                Console.Error.WriteLine("Resize operation failed.");
            }
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}