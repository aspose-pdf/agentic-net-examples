using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PDF exists before trying to bind it.
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Input PDF not found: {Path.GetFullPath(inputPdfPath)}");
            return;
        }

        // A tiny 1×1 PNG encoded as Base64 – this removes the dependency on an external image file.
        const string pngBase64 = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mP8z8DwHwAFBQIAX8jx0gAAAABJRU5ErkJggg==";
        byte[] pngBytes = Convert.FromBase64String(pngBase64);

        // Load the PNG into a memory stream (the stream must stay open while AddImage is called).
        using (MemoryStream pngStream = new MemoryStream(pngBytes))
        {
            // PdfFileMend does not implement IDisposable, so we instantiate it directly.
            PdfFileMend mend = new PdfFileMend();

            // Bind the existing PDF document.
            mend.BindPdf(inputPdfPath);

            // Define the target page(s) and rectangle where the image will be placed.
            int[] pages = { 24 };                     // page 24 (1‑based index)
            float llx = 120f;                         // lower‑left X
            float lly = 120f;                         // lower‑left Y
            float urx = 320f;                         // upper‑right X (120 + 200)
            float ury = 320f;                         // upper‑right Y (120 + 200)

            // Add the image from the memory stream.
            mend.AddImage(pngStream, pages, llx, lly, urx, ury);

            // Save the modified PDF.
            mend.Save(outputPdfPath);
        }

        Console.WriteLine($"PNG image added to page 24 and saved as '{outputPdfPath}'.");
    }
}