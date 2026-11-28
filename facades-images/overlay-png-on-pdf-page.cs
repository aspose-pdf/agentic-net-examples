using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pngPath = "overlay.png";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(pngPath))
        {
            Console.Error.WriteLine($"PNG not found: {pngPath}");
            return;
        }

        // PdfFileMend does not implement IDisposable, so no using block.
        PdfFileMend mend = new PdfFileMend();

        // Bind the existing PDF.
        mend.BindPdf(pdfPath);

        // CompositingParameters requires a BlendMode argument.
        // For a simple overlay we can use BlendMode.Normal.
        CompositingParameters compParams = new CompositingParameters(BlendMode.Normal);

        // Add the PNG image onto page 36 (pages are 1‑based).
        // Use the overload that accepts an int[] of page numbers and explicit coordinates/sizes.
        int targetPage = 36;
        int[] pages = new int[] { targetPage };
        // Position (X, Y) and size (Width, Height) are expressed in points.
        // Adjust these values as needed for your specific layout.
        float posX = 100f;   // distance from the left edge
        float posY = 200f;   // distance from the bottom edge
        float width = 500f;  // desired image width
        float height = 650f; // desired image height

        mend.AddImage(pngPath, pages, posX, posY, width, height, compParams);

        // Save the modified PDF.
        mend.Save(outputPath);

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
