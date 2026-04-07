using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPath = "logo.png";

        // Verify that the source PDF and logo image exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a stamp and bind the PNG logo as its image
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(logoPath);

        // Set semi‑transparent opacity (0.5 = 50%)
        stamp.Opacity = 0.5f;

        // Rotate the stamp 30 degrees clockwise
        stamp.Rotation = 30f; // arbitrary angle in degrees

        // Place the stamp behind the page content (optional)
        stamp.IsBackground = true;

        // Add the stamp to all pages of the document
        fileStamp.AddStamp(stamp);

        // Save the modified PDF and release resources
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Watermark annotation added and saved to '{outputPdf}'.");
    }
}