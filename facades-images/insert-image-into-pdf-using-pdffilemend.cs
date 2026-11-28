using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string imagePath = "image.jpg";
        const string outputPdf = "output.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // PdfFileMend does not implement IDisposable, instantiate directly.
            PdfFileMend mend = new PdfFileMend();

            // Bind the source PDF document.
            mend.BindPdf(sourcePdf);

            // Define the rectangle where the image will be placed (lower‑left X/Y and width/height).
            float llx = 100f; // lower‑left X (points)
            float lly = 500f; // lower‑left Y (points)
            float urx = 300f; // upper‑right X (points)
            float ury = 700f; // upper‑right Y (points)
            float width = urx - llx;
            float height = ury - lly;

            // AddImage expects a Stream for the image, an int for the page number, and float values for coordinates.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Insert on page 1 (pages are 1‑based in Aspose.Pdf).
                mend.AddImage(imgStream, 1, llx, lly, width, height);
            }

            // Save the modified PDF to the output file.
            mend.Save(outputPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
