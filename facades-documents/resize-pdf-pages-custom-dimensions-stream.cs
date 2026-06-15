using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // 1 inch = 72 points (default Aspose.Pdf unit)
        const double inchesToPoints = 72.0;
        double pageWidth  = 8.5 * inchesToPoints; // 612 points
        double pageHeight = 11.0 * inchesToPoints; // 792 points

        // Create resize parameters that set the new page size
        var resizeParams = PdfFileEditor.ContentsResizeParameters.PageResize(pageWidth, pageHeight);

        // PdfFileEditor does NOT implement IDisposable – instantiate directly
        var editor = new PdfFileEditor();

        // Use stream overload to resize pages
        using (FileStream srcStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream destStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Resize all pages (pages argument = null) using the prepared parameters
            bool success = editor.ResizeContents(srcStream, destStream, null, resizeParams);

            if (!success)
            {
                Console.Error.WriteLine("Failed to resize PDF pages.");
                return;
            }
        }

        Console.WriteLine($"PDF pages resized to 8.5\" x 11\" and saved to '{outputPath}'.");
    }
}