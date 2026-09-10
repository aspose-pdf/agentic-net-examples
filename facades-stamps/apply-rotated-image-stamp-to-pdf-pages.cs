using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";   // source PDF
        const string outputPdf  = "output.pdf";  // result PDF
        const string stampImage = "stamp.png";   // image to use as stamp

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

        // Load the source PDF to obtain page dimensions.
        Document srcDoc = new Document(inputPdf);

        // Initialize the facade and bind the source PDF.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Desired stamp size (in points) and margins.
        const float stampWidth  = 80f;   // width of the stamp image
        const float stampHeight = 80f;   // height of the stamp image
        const float rightMargin = 10f;   // distance from the right edge
        const float bottomMargin = 10f;  // distance from the bottom edge
        const float rotationAngle = 30f; // rotation in degrees

        // Iterate over every page and add a rotated image stamp to the bottom‑right corner.
        foreach (Page page in srcDoc.Pages)
        {
            // Create a new stamp for the current page.
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(stampImage);          // use the image as stamp
            stamp.SetImageSize(stampWidth, stampHeight);
            stamp.Rotation = rotationAngle;

            // Calculate the origin (lower‑left corner of the stamp) so that the stamp
            // sits at the bottom‑right corner of the page.
            // Page coordinates: (0,0) is lower‑left.
            float originX = (float)page.PageInfo.Width - rightMargin - stampWidth;
            float originY = bottomMargin; // distance from bottom edge
            stamp.SetOrigin(originX, originY);

            // Apply the stamp only to the current page.
            stamp.Pages = new int[] { page.Number };
            fileStamp.AddStamp(stamp);
        }

        // Save the result and release resources.
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Rotated image stamp applied. Output saved to '{outputPdf}'.");
    }
}
