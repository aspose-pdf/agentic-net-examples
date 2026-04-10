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
        const string stampImage = "stamp.png";

        // Verify required files exist
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

        // Use PdfFileStamp facade to add a rotated stamp
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF document
            fileStamp.BindPdf(inputPdf);

            // Create a stamp and bind the image to it
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(stampImage);

            // Position the stamp on the page (example coordinates)
            stamp.SetOrigin(100, 100);
            // Optionally set the stamp size
            stamp.SetImageSize(200, 200);

            // Rotate the stamp 90 degrees clockwise
            stamp.Rotation = 90f;

            // Add the stamp to the PDF
            fileStamp.AddStamp(stamp);

            // Save the resulting PDF
            fileStamp.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}