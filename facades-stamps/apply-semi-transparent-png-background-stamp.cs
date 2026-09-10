using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImage = "stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Initialize the facade for stamping
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Load the source PDF
            fileStamp.BindPdf(inputPdf);

            // Create a stamp and bind the PNG image
            Stamp stamp = new Stamp();
            stamp.BindImage(stampImage);

            // Make the stamp semi‑transparent
            stamp.Opacity = 0.5f;

            // Place the stamp behind the page content
            stamp.IsBackground = true;

            // Resize the stamp to cover the whole page
            stamp.SetImageSize((float)fileStamp.PageWidth, (float)fileStamp.PageHeight);

            // Apply the stamp to all pages (null means all pages)
            stamp.Pages = null;

            // Add the stamp to the document
            fileStamp.AddStamp(stamp);

            // Save the stamped PDF
            fileStamp.Save(outputPdf);
        }

        Console.WriteLine($"Background stamp applied and saved to '{outputPdf}'.");
    }
}