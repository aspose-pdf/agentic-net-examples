using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF containing the JPEG
        const string outputPdfPath = "output.pdf";     // PDF after replacement
        const string bmpImagePath  = "highres.bmp";    // higher‑resolution BMP to insert

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(bmpImagePath))
        {
            Console.Error.WriteLine($"BMP image not found: {bmpImagePath}");
            return;
        }

        // PdfContentEditor does not implement IDisposable, so no using block is needed.
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the source PDF
        editor.BindPdf(inputPdfPath);

        // Replace the first image on page 1 (imageIndex is 1‑based)
        // Use the overload that accepts a file path (string).
        editor.ReplaceImage(1, 1, bmpImagePath);

        // Save the modified PDF
        editor.Save(outputPdfPath);

        Console.WriteLine($"Image replaced and saved to '{outputPdfPath}'.");
    }
}
