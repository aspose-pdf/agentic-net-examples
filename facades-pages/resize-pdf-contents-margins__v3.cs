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

        // Create resize parameters with left/right margins of 10 points,
        // top margin of 20 points and bottom margin of 30 points.
        var resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(10, 10, 20, 30);

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPath))
        {
            // PdfFileEditor does NOT implement IDisposable and has no Close method.
            PdfFileEditor pdfEditor = new PdfFileEditor();

            // Resize contents of all pages (null page array means all pages).
            pdfEditor.ResizeContents(pdfDoc, null, resizeParams);

            // Save the modified document.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
