using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string image1Path = "image1.jpg";
        const string image2Path = "image2.png";

        // Verify that all required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(image1Path))
        {
            Console.Error.WriteLine($"Image 1 not found: {image1Path}");
            return;
        }
        if (!File.Exists(image2Path))
        {
            Console.Error.WriteLine($"Image 2 not found: {image2Path}");
            return;
        }

        try
        {
            // PdfFileMend does not implement IDisposable, so no using block is needed
            PdfFileMend mend = new PdfFileMend();

            // Bind the source PDF document
            mend.BindPdf(inputPdf);

            // Add first image to page 1 at (100, 500) with a size of 200x200 points
            mend.AddImage(
                image1Path,
                new int[] { 1 },          // target pages (1‑based)
                100f,                     // left margin (X coordinate)
                500f,                     // top margin (Y coordinate from bottom of page)
                200f,                     // width of the image
                200f);                    // height of the image

            // Add second image to page 2 at (200, 300) with a size of 150x150 points
            mend.AddImage(
                image2Path,
                new int[] { 2 },
                200f,
                300f,
                150f,
                150f);

            // Save the modified PDF to the output path
            mend.Save(outputPdf);

            Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
