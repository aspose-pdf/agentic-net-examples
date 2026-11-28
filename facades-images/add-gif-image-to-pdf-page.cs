using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string gifImage = "image.gif";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(gifImage))
        {
            Console.Error.WriteLine($"Image file not found: {gifImage}");
            return;
        }

        // PdfFileMend does not implement IDisposable, so no using block is needed.
        PdfFileMend mend = new PdfFileMend();

        // Bind the source PDF.
        mend.BindPdf(inputPdf);

        // Add the GIF image to page 17 at coordinates (200,200).
        // Use the overload that accepts an array of page numbers and the image rectangle (lower‑left X/Y, upper‑right X/Y).
        // Here we place the image with a width and height of 200 points (i.e., lower‑left (200,200), upper‑right (400,400)).
        mend.AddImage(gifImage, new int[] { 17 }, 200f, 200f, 400f, 400f);

        // Save the modified PDF.
        mend.Save(outputPdf);

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}
