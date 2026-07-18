using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string stampImage = "background.png";

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

        // Load the source PDF to obtain page dimensions (assumes all pages share the same size)
        double pageWidth, pageHeight;
        using (Document doc = new Document(inputPdf))
        {
            pageWidth  = doc.Pages[1].PageInfo.Width;
            pageHeight = doc.Pages[1].PageInfo.Height;
        }

        // Initialize the facade for stamping
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF
            fileStamp.BindPdf(inputPdf);

            // Create and configure the stamp
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(stampImage);                     // PNG image to use as stamp
            stamp.SetImageSize((float)pageWidth, (float)pageHeight); // Cover entire page
            stamp.SetOrigin(0, 0);                           // Position at lower‑left corner
            stamp.Opacity = 0.5f;                            // Semi‑transparent
            stamp.IsBackground = true;                       // Place behind page content

            // Add the stamp to all pages
            fileStamp.AddStamp(stamp);

            // Save the stamped PDF
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Background stamp applied and saved to '{outputPdf}'.");
    }
}