using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // required for CompositingParameters and BlendMode

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.jpg";

        // Verify that the required files exist.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // PdfFileMend is a facade class; it does not implement IDisposable.
        PdfFileMend mend = new PdfFileMend();

        // Load the source PDF.
        mend.BindPdf(inputPdf);

        // Define the rectangle where the image will be placed (lower‑left corner + size).
        float llx = 100f;   // lower‑left X coordinate
        float lly = 100f;   // lower‑left Y coordinate
        float width = 200f; // desired width
        float height = 200f; // desired height

        // CompositingParameters requires a BlendMode in its constructor.
        // No PreserveAspectRatio property exists; the image will be scaled to the rectangle.
        var compParams = new CompositingParameters(BlendMode.Normal);

        // Add the image to page 6. The overload expects:
        //   (string imagePath, int[] pageNumbers, float x, float y, float width, float height, CompositingParameters)
        mend.AddImage(imagePath, new int[] { 6 }, llx, lly, width, height, compParams);

        // Save the modified PDF.
        mend.Save(outputPdf);

        Console.WriteLine($"Image added to page 6 and saved as '{outputPdf}'.");
    }
}
