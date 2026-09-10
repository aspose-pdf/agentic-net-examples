using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImagePath = "stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp image not found: {stampImagePath}");
            return;
        }

        // Initialize the facade and bind the source PDF.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);               // Load source PDF

        // Create a stamp that will be applied to all pages.
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(stampImagePath);           // Use an image as the stamp
        stamp.IsBackground = true;                // Place stamp behind page content
        stamp.Opacity = 0.5f;                      // Semi‑transparent
        stamp.SetOrigin(100, 500);                // Position (X, Y) from lower‑left corner
        stamp.SetImageSize(150, 100);             // Desired width and height

        // Leave stamp.Pages = null (default) so the stamp affects every page.
        fileStamp.AddStamp(stamp);                 // Add the stamp to the document

        // Save the result and release resources.
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp applied to all pages. Output saved to '{outputPdf}'.");
    }
}