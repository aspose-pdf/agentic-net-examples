using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string stampImagePath = "stamp.tif";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Bind the source PDF.
        var pdfStamp = new PdfFileStamp();
        pdfStamp.BindPdf(inputPdfPath);

        // Create a stamp from the image stream.
        using (FileStream imgStream = File.OpenRead(stampImagePath))
        {
            var stamp = new Stamp();
            stamp.BindImage(imgStream);          // use the image as stamp content

            // Position the stamp 30 points above the bottom edge.
            // SetOrigin expects coordinates measured from the lower‑left corner.
            stamp.SetOrigin(0, 30); // X = 0 (left), Y = 30 (bottom margin)

            // Apply the stamp to all pages.
            pdfStamp.AddStamp(stamp);
        }

        // Save the stamped PDF.
        pdfStamp.Save(outputPdfPath);
    }
}
