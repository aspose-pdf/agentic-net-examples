using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string imagePath = "logo.png";
        const string outputPdf = "output.pdf";

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

        // PdfFileMend implements IDisposable, so wrap it in a using block.
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Load the source PDF.
            mend.BindPdf(inputPdf);

            // Define image placement coordinates (lower‑left and upper‑right corners).
            // Example: place the image at (100, 500) with a width of 200 and height of 100.
            int llx = 100; // lower‑left X
            int lly = 500; // lower‑left Y
            int urx = 300; // upper‑right X (llx + width)
            int ury = 600; // upper‑right Y (lly + height)

            // Add the image to page 1 (pages are 1‑based in Aspose.Pdf).
            // Use a FileStream for the image because the overload expects a Stream.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                mend.AddImage(imgStream, llx, lly, urx, ury, 1);
            }

            // Save the modified PDF.
            mend.Save(outputPdf);
        }

        Console.WriteLine($"Image inserted and saved to '{outputPdf}'.");
    }
}
