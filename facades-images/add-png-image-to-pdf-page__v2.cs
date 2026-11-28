using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";   // source PDF
        const string pngPath = "image.png";   // PNG to insert
        const string outputPath = "output.pdf"; // result PDF

        // Verify that source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(pngPath))
        {
            Console.Error.WriteLine($"Image not found: {pngPath}");
            return;
        }

        // PdfFileMend does NOT implement IDisposable, so no using block is required
        PdfFileMend mend = new PdfFileMend();

        // Load (bind) the existing PDF document
        mend.BindPdf(pdfPath);

        // Add the PNG image to page 18 at coordinates (300,300).
        // The AddImage overload expects an array of page numbers and also width/height values.
        // Here we use a simple width and height of 200 points each; adjust as needed.
        int[] pages = new int[] { 18 };
        float left = 300f;   // X coordinate
        float bottom = 300f; // Y coordinate (from bottom of the page)
        float width = 200f;  // Desired image width
        float height = 200f; // Desired image height
        mend.AddImage(pngPath, pages, left, bottom, width, height);

        // Save the modified PDF
        mend.Save(outputPath);

        Console.WriteLine($"Image added and PDF saved to '{outputPath}'.");
    }
}
