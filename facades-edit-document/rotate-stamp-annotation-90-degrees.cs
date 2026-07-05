using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class RotateStampExample
{
    static void Main()
    {
        // Paths for the source PDF, the image to be used as a stamp, and the output PDF.
        const string inputPdfPath   = "input.pdf";
        const string stampImagePath = "stampImage.png";
        const string outputPdfPath  = "output_rotated_stamp.pdf";

        // Verify that the required files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp image not found: {stampImagePath}");
            return;
        }

        // Initialize the PdfFileStamp facade.
        // The facade does not implement IDisposable, so we manage its lifetime manually.
        PdfFileStamp fileStamp = new PdfFileStamp();

        try
        {
            // Bind the source PDF document to the facade.
            fileStamp.BindPdf(inputPdfPath);

            // Create a new Aspose.Pdf.Facades.Stamp object.
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Bind the image that will be used as the stamp content.
            stamp.BindImage(stampImagePath);

            // Rotate the stamp 90 degrees clockwise to match the underlying image orientation.
            // The Rotation property expects a value in degrees.
            stamp.Rotation = 90f;

            // Optionally, set the stamp to appear on top of the page content.
            stamp.IsBackground = false;

            // Add the configured stamp to the PDF.
            fileStamp.AddStamp(stamp);

            // Save the resulting PDF with the rotated stamp.
            fileStamp.Save(outputPdfPath);
        }
        finally
        {
            // Ensure resources are released.
            fileStamp.Close();
        }

        Console.WriteLine($"PDF saved with rotated stamp: {outputPdfPath}");
    }
}