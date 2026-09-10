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
        const string imagePath = "stampImage.png";

        // Verify that the source PDF and image exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the facade with input and output files
        PdfFileStamp fileStamp = new PdfFileStamp(inputPdf, outputPdf);

        // Create a stamp, bind the image, and configure its appearance
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imagePath);   // use the specified image as stamp content
        stamp.Rotation = 45;          // rotate the stamp by 45 degrees
        stamp.Opacity = 0.8f;         // set opacity to 80%
        stamp.SetOrigin(100, 200);    // place the stamp at (100,200) on the page

        // Apply the stamp to all pages (null means all pages)
        stamp.Pages = null;

        // Add the configured stamp to the PDF
        fileStamp.AddStamp(stamp);

        // Save changes and release resources
        fileStamp.Close();

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}