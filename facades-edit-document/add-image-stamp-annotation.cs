using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // required for Rectangle and Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "stamp_image.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Bind the PDF document.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // System.Drawing.Rectangle expects (x, y, width, height).
        // Original Aspose rectangle (LLX, LLY, URX, URY) = (100, 500, 300, 600)
        // => width = 300 - 100 = 200, height = 600 - 500 = 100
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 100);

        // Create a rubber‑stamp annotation on page 2 using the image as the appearance.
        // The overload expects System.Drawing.Rectangle and System.Drawing.Color.
        editor.CreateRubberStamp(2, rect, "ImageStamp", imagePath, System.Drawing.Color.Black);

        // Save the modified PDF.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Stamp annotation added to page 2 and saved as '{outputPath}'.");
    }
}