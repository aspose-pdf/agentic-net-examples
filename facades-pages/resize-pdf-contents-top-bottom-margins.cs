using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Desired margins (in points)
        double leftMargin   = 0.0;   // no change on the left side
        double rightMargin  = 0.0;   // no change on the right side
        double topMargin    = 50.0;  // top margin to add
        double bottomMargin = 30.0;  // bottom margin to add

        // Create resize parameters via the static Margins method (left, right, top, bottom)
        var resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(
            leftMargin, rightMargin, topMargin, bottomMargin);

        // Load the PDF, apply the resize, and save the result
        using (Document doc = new Document(inputPath))
        {
            PdfFileEditor editor = new PdfFileEditor();
            editor.ResizeContents(doc, resizeParams);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
