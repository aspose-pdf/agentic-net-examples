using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the PNG image, and the output PDF
        const string inputPdfPath  = "input.pdf";
        const string imagePath     = "image.png";
        const string outputPdfPath = "output.pdf";

        // Coordinates where the image will be placed on page 2
        // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
        const float llx = 100f; // left X
        const float lly = 200f; // lower Y
        const float urx = 300f; // right X
        const float ury = 400f; // upper Y

        // ------------------------------------------------------------
        // Ensure a source PDF exists – create a minimal two‑page PDF
        // if the file is missing. This makes the example self‑contained.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using var placeholderDoc = new Document();
            placeholderDoc.Pages.Add(); // page 1
            placeholderDoc.Pages.Add(); // page 2 (target page)
            placeholderDoc.Save(inputPdfPath);
        }

        // ------------------------------------------------------------
        // Ensure a PNG image exists – write a 1×1 pixel PNG from a
        // Base64 string if the file is missing.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            // Minimal transparent 1x1 PNG (Base64 encoded)
            const string base64Png = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII=";
            byte[] pngBytes = Convert.FromBase64String(base64Png);
            File.WriteAllBytes(imagePath, pngBytes);
        }

        // Create a PdfFileMend instance (does NOT implement IDisposable)
        var pdfMend = new PdfFileMend();

        // Bind the existing PDF document (expects a Stream)
        using (FileStream pdfStream = File.OpenRead(inputPdfPath))
        {
            pdfMend.BindPdf(pdfStream);
        }

        // Add the PNG image to page 2 at the specified rectangle (expects a Stream and float values)
        using (FileStream imgStream = File.OpenRead(imagePath))
        {
            pdfMend.AddImage(imgStream, 2, llx, lly, urx, ury);
        }

        // Save the modified PDF
        pdfMend.Save(outputPdfPath);
    }
}
