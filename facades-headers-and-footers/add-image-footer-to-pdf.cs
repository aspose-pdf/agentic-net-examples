using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string signaturePath = "signature.jpg";

        // Verify required files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(signaturePath))
        {
            Console.Error.WriteLine($"Signature image not found: {signaturePath}");
            return;
        }

        // Create the stamp that will hold the image.
        var imageStamp = new Aspose.Pdf.Facades.Stamp();
        // Bind the image from a FileStream as required.
        using (FileStream imgStream = File.OpenRead(signaturePath))
        {
            imageStamp.BindImage(imgStream);
        }
        // Position the stamp: left‑aligned (x = 0) and 40 points above the bottom edge.
        imageStamp.SetOrigin(0, 40);
        // By default the stamp is applied to all pages; no need to set Pages.

        // Apply the stamp to the PDF.
        using (var pdfStamp = new PdfFileStamp())
        {
            pdfStamp.BindPdf(inputPdfPath);
            pdfStamp.AddStamp(imageStamp);
            pdfStamp.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}
