using System;
using System.IO;
using System.Drawing;                     // For System.Drawing.Color
using Aspose.Pdf;                         // Core PDF classes
using Aspose.Pdf.Facades;                 // Facade classes for stamping

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // Source PDF
        const string outputPdf  = "output.pdf";     // Result PDF with stamp
        const string logoPath   = "logo.png";       // Company logo image file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // ---------- Image stamp ----------
        // Create a stamp that will display the company logo.
        Aspose.Pdf.Facades.Stamp imageStamp = new Aspose.Pdf.Facades.Stamp();
        imageStamp.BindImage(logoPath);               // Set the image to be stamped
        imageStamp.SetOrigin(100, 700);               // Position (X, Y) from lower‑left corner
        imageStamp.SetImageSize(100, 50);             // Width and height of the image
        imageStamp.Opacity = 0.8f;                    // Slightly transparent
        imageStamp.IsBackground = true;              // Place behind page content
        fileStamp.AddStamp(imageStamp);               // Add the image stamp

        // ---------- Text stamp ----------
        // Create formatted text: "Confidential" in bold, red, 36‑pt font.
        Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
            "Confidential",                         // Text
            System.Drawing.Color.Red,                // Text color
            "Helvetica-Bold",                       // Font name (bold variant)
            Aspose.Pdf.Facades.EncodingType.Winansi, // Encoding
            false,                                    // Do not embed the font
            36f);                                     // Font size (float)

        // Bind the formatted text to a stamp.
        Aspose.Pdf.Facades.Stamp textStamp = new Aspose.Pdf.Facades.Stamp();
        textStamp.BindLogo(ft);                      // Bind the text as a logo
        textStamp.SetOrigin(120, 720);               // Position the text near the logo
        textStamp.Opacity = 0.9f;
        textStamp.IsBackground = false;             // Render on top of page content
        fileStamp.AddStamp(textStamp);               // Add the text stamp

        // Save the stamped PDF to the output file.
        fileStamp.Save(outputPdf);
        fileStamp.Close(); // Ensure all resources are released

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
