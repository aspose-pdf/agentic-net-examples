using System;
using System.IO;
using System.Drawing;               // Required for System.Drawing.Color
using Aspose.Pdf.Facades;          // PdfFileStamp, Stamp, FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // Source PDF
        const string outputPdf = "stamped_output.pdf"; // Destination PDF
        const string logoPath  = "logo.png";   // Company logo image

        // Verify that required files exist
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

        // Use PdfFileStamp facade to load, stamp, and save the document
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Load the source PDF
            fileStamp.BindPdf(inputPdf);

            // Create a new stamp instance
            Stamp stamp = new Stamp();

            // Position the stamp (lower‑left corner coordinates, in points)
            stamp.SetOrigin(100, 500);          // Adjust X/Y as needed

            // Make the stamp appear behind page content
            stamp.IsBackground = true;

            // Set semi‑transparent opacity
            stamp.Opacity = 0.5f;

            // Bind the company logo image to the stamp
            stamp.BindImage(logoPath);
            // Optionally define the image size (width, height)
            stamp.SetImageSize(100, 100);       // Adjust size as needed

            // Create formatted text for the word "Confidential" in bold
            FormattedText ft = new FormattedText(
                "Confidential",                 // Text content
                Color.Red,                      // Text color
                "Helvetica-Bold",               // Bold font (use a bold variant)
                EncodingType.Winansi,           // Text encoding
                false,                          // Do not embed the font
                36);                            // Font size

            // Bind the formatted text to the same stamp
            stamp.BindLogo(ft);

            // Apply the stamp to all pages of the document
            fileStamp.AddStamp(stamp);

            // Save the stamped PDF
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}