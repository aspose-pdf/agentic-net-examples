using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF and image files
        const string pdfPath   = "input.pdf";
        const string pngPath   = "background.png"; // PNG to be placed on page 3
        const string gifPath   = "overlay.gif";    // GIF to overlay on the PNG
        const string outputPath = "output.pdf";

        // Verify that all required files exist
        if (!File.Exists(pdfPath) || !File.Exists(pngPath) || !File.Exists(gifPath))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // Bind the PDF for editing using PdfFileMend (facade API)
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(pdfPath);

        // --------------------------------------------------------------------
        // 1) Add the PNG image to page 3 (acts as a background image)
        // --------------------------------------------------------------------
        // CompositingParameters must be created with a BlendMode argument.
        var pngCompositing = new CompositingParameters(BlendMode.Normal);
        // AddImage overload expects: imagePath, pageNumbers[], x, y, width, height, parameters
        // Position (0,0) with size 595x842 covers a typical A4 page.
        mend.AddImage(pngPath, new int[] { 3 }, 0f, 0f, 595f, 842f, pngCompositing);

        // --------------------------------------------------------------------
        // 2) Overlay the GIF image onto the same page using compositing
        // --------------------------------------------------------------------
        var gifCompositing = new CompositingParameters(BlendMode.Normal);
        // Example position and size for the overlay (adjust as needed).
        // Here we place the GIF at (100,600) with a width of 200 and height of 200.
        mend.AddImage(gifPath, new int[] { 3 }, 100f, 600f, 200f, 200f, gifCompositing);

        // --------------------------------------------------------------------
        // Save the modified PDF to the output file
        // --------------------------------------------------------------------
        mend.Save(outputPath);

        Console.WriteLine($"PDF saved with PNG background and GIF overlay: {outputPath}");
    }
}
