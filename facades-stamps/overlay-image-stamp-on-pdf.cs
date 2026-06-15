using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for input PDF, stamp image (or any other stamp source), and output PDF.
        const string inputPdf  = "input.pdf";
        const string stampImg  = "stamp.png";
        const string outputPdf = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp image not found: {stampImg}");
            return;
        }

        // Initialize the PdfFileStamp facade.
        // Use the parameterless constructor and bind the source PDF via BindPdf().
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf); // Load the document to be stamped.

        // Create a Aspose.Pdf.Facades.Stamp object (from the Facades namespace) and configure it.
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind an image as the stamp content. You could also use BindLogo or BindPdf.
        stamp.BindImage(stampImg);

        // Ensure the stamp overlays existing page content (IsBackground = false is the default,
        // but we set it explicitly for clarity).
        stamp.IsBackground = false;

        // Optionally adjust stamp position, size, opacity, etc.
        stamp.SetOrigin(100, 200);          // X and Y coordinates (bottom‑left origin).
        stamp.SetImageSize(150, 100);       // Width and height of the image stamp.
        stamp.Opacity = 0.8f;               // Semi‑transparent overlay.

        // Add the configured stamp to the PDF.
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF to the desired output file.
        fileStamp.Save(outputPdf);

        // Close the facade to release resources.
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}