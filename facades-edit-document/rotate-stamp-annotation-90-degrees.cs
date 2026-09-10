using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImage = "stamp.jpg";

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
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdf);

            // Create a stamp, bind the image, and rotate it 90 degrees clockwise
            Stamp stamp = new Stamp();
            stamp.BindImage(stampImage);
            stamp.Rotation = 90f; // clockwise rotation

            // Optional: set position and size of the stamp
            stamp.SetOrigin(100, 100);
            stamp.SetImageSize(200, 200);
            stamp.IsBackground = false; // place stamp above page content

            // Add the configured stamp to the PDF
            fileStamp.AddStamp(stamp);

            // Save the modified PDF
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}