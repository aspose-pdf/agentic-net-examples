using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "background.png";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Background image not found: {imagePath}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdf);

            // Create a stamp, bind the image, and configure it as a background
            Stamp stamp = new Stamp();
            stamp.BindImage(imagePath);
            stamp.IsBackground = true;               // place behind page content

            // Set blend mode to Multiply if the property exists.
            // The BlendMode property is not listed in the core documentation,
            // but many versions expose it. Uncomment the line below if available.
            // stamp.BlendMode = BlendMode.Multiply;

            // If BlendMode is unavailable, you can set the blending color space
            // as a fallback (does not change the mode but keeps the code valid).
            // stamp.BlendingSpace = BlendingColorSpace.DeviceRGB;

            // Apply the stamp to all pages of the document
            fileStamp.AddStamp(stamp);

            // Save the modified PDF
            fileStamp.Save(outputPdf);
            // Close releases resources; using statement will also call Dispose()
            fileStamp.Close();
        }

        Console.WriteLine($"Background image added and saved to '{outputPdf}'.");
    }
}