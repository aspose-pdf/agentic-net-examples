using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the image to be used as a stamp.
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImg  = "stamp.png";

        // Verify that required files exist.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp image not found: {stampImg}");
            return;
        }

        // Use PdfFileStamp (facade) to apply a single stamp to all pages.
        // The stamp will be applied to every page because the Pages property
        // remains null (default behavior).
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF document.
            fileStamp.BindPdf(inputPdf);

            // Create the stamp object.
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Bind an image to the stamp.
            stamp.BindImage(stampImg);

            // Position the stamp (origin is measured from the lower‑left corner).
            stamp.SetOrigin(100, 500);          // X = 100, Y = 500

            // Define the size of the stamp on the page.
            stamp.SetImageSize(200, 100);       // Width = 200, Height = 100

            // Optional: make the stamp semi‑transparent and place it behind content.
            stamp.Opacity = 0.6f;               // 60 % opacity
            stamp.IsBackground = true;         // render as background

            // Add the stamp to the document. Because stamp.Pages is null,
            // the stamp is applied to all pages efficiently (single operation).
            fileStamp.AddStamp(stamp);

            // Save the result to the output file.
            fileStamp.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}