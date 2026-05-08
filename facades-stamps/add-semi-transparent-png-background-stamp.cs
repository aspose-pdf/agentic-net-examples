using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade APIs for stamping

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string stampImage = "stamp.png"; // semi‑transparent PNG

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);               // Load source PDF

        // Create a stamp that uses the PNG image
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(stampImage);               // Set image source
        stamp.IsBackground = true;                 // Place behind page content
        stamp.Opacity = 0.5f;                      // Semi‑transparent (0.0 – 1.0)

        // Size the stamp to cover the whole page
        // PageWidth / PageHeight are available after BindPdf()
        stamp.SetOrigin(0, 0);                     // Lower‑left corner
        stamp.SetImageSize(fileStamp.PageWidth, fileStamp.PageHeight);

        // Add the stamp to all pages
        fileStamp.AddStamp(stamp);

        // Save the result and release resources
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Background stamp applied and saved to '{outputPdf}'.");
    }
}