using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // 8.5 x 11 inches in points (72 points per inch)
        double pageWidth = 8.5 * 72.0;
        double pageHeight = 11.0 * 72.0;

        using (FileStream srcStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream destStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor fileEditor = new PdfFileEditor();
            // null pages means all pages are processed
            bool success = fileEditor.ResizeContents(srcStream, destStream, null, pageWidth, pageHeight);
            if (!success)
            {
                Console.Error.WriteLine("Resize operation failed.");
            }
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
