using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.png";

        // Desired rectangle for the image on page 2 (lower‑left and upper‑right coordinates)
        // Aspose.Pdf.Facades.PdfFileMend.AddImage expects float values, so use float literals.
        float llx = 100f; // lower‑left X
        float lly = 200f; // lower‑left Y
        float urx = 300f; // upper‑right X (llx + width)
        float ury = 350f; // upper‑right Y (lly + height)

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

        // PdfFileMend does not implement IDisposable, so a using block is not required.
        PdfFileMend mend = new PdfFileMend();

        // Load (bind) the existing PDF.
        mend.BindPdf(inputPdf);

        // Add the PNG image to page 2 at the specified rectangle.
        // The AddImage overload expects float parameters, which we provide above.
        using (FileStream imgStream = File.OpenRead(imagePath))
        {
            mend.AddImage(imgStream, 2, llx, lly, urx, ury);
        }

        // Save the modified PDF.
        mend.Save(outputPdf);

        Console.WriteLine($"Image added to page 2 and saved as '{outputPdf}'.");
    }
}
