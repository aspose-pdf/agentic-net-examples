using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // source PDF
        const string imagePath = "image.jpg";     // JPG to insert
        const string outputPdfPath = "output.pdf"; // result PDF

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // PdfFileMend does NOT implement IDisposable – do NOT wrap in using
        PdfFileMend mend = new PdfFileMend();

        // Bind the existing PDF document
        mend.BindPdf(inputPdfPath);

        // Add the JPG image to page 19.
        // Coordinates are specified as lower‑left (llx, lly) and upper‑right (urx, ury).
        // Here the image lower‑left corner is placed at (400,400) and its size is set to 200×200.
        float llx = 400f;
        float lly = 400f;
        float urx = 600f; // llx + width (200)
        float ury = 600f; // lly + height (200)

        // AddImage expects a Stream for the image and float values for the rectangle.
        using (FileStream imgStream = File.OpenRead(imagePath))
        {
            mend.AddImage(imgStream, 19, llx, lly, urx, ury);
        }

        // Save the modified PDF
        mend.Save(outputPdfPath);

        Console.WriteLine($"Image added and PDF saved to '{outputPdfPath}'.");
    }
}
