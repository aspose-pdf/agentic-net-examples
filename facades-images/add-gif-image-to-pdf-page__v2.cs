using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string gifImagePath  = "image.gif";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(gifImagePath))
        {
            Console.Error.WriteLine($"GIF image not found: {gifImagePath}");
            return;
        }

        // PdfFileMend does NOT implement IDisposable, so no using block is needed
        PdfFileMend mend = new PdfFileMend();

        // Bind the source PDF document
        mend.BindPdf(inputPdfPath);

        // Add the GIF image to page 44 at coordinates (180,180).
        // PdfFileMend.AddImage expects an int[] for page numbers and four float values
        // representing the lower‑left (x, y) and upper‑right (x, y) corners of the image.
        // Here we place the image with a 100 pt width and height.
        mend.AddImage(
            gifImagePath,                 // image file path (can also be a Stream)
            new int[] { 44 },            // target pages (1‑based)
            180f,                        // lower‑left X
            180f,                        // lower‑left Y
            280f,                        // upper‑right X (180 + 100)
            280f);                       // upper‑right Y (180 + 100)

        // Save the modified PDF to the output file
        mend.Save(outputPdfPath);

        Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");
    }
}
