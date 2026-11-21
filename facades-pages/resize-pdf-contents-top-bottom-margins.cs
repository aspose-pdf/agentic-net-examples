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

        // Margins are specified in points (1 point = 1/72 inch)
        double topMargin = 50;    // top margin
        double bottomMargin = 30; // bottom margin

        // Load the source PDF using the high‑level Document API
        Document pdfDoc = new Document(inputPath);

        // Prepare resize parameters – left and right margins are 0, top/bottom are as required
        // PdfFileEditor.ContentsResizeParameters is a nested type; use the static Margins factory method.
        var resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(
            left: 0,
            right: 0,
            top: topMargin,
            bottom: bottomMargin);

        // Use PdfFileEditor to apply the resize operation. Passing null for pageNumbers applies to all pages.
        PdfFileEditor editor = new PdfFileEditor();
        editor.ResizeContents(pdfDoc, null, resizeParams);

        // Save the modified PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"PDF saved with adjusted margins to '{outputPath}'.");
    }
}
