using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF that already contains a PNG image
        const string outputPdf = "output.pdf";  // Resulting PDF with GIF overlay
        const string gifPath   = "overlay.gif"; // Semi‑transparent GIF to overlay

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(gifPath))
        {
            Console.Error.WriteLine($"Overlay GIF not found: {gifPath}");
            return;
        }

        // Create the PdfFileMend facade (no obsolete constructor)
        PdfFileMend mend = new PdfFileMend();

        // Bind the source PDF document
        mend.BindPdf(inputPdf);

        // Prepare compositing parameters.
        // BlendMode.Normal respects the GIF's own alpha channel (semi‑transparent).
        CompositingParameters compParams = new CompositingParameters(BlendMode.Normal);

        // Add the GIF image to page 1.
        // Coordinates are in points (1/72 inch). Adjust as needed to match the PNG area.
        // lowerLeftX, lowerLeftY, upperRightX, upperRightY
        mend.AddImage(gifPath, 1, 50f, 500f, 250f, 700f, compParams);

        // Save the modified PDF
        mend.Save(outputPdf);

        // Release resources
        mend.Close();

        Console.WriteLine($"Overlay applied and saved to '{outputPdf}'.");
    }
}