using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPath = "logo.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdf);

            // Create a stamp instance (fully qualified to avoid ambiguity)
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Bind the company logo image to the stamp
            stamp.BindImage(logoPath);
            // Optionally set the image size (width, height)
            stamp.SetImageSize(100f, 50f);

            // Create formatted text for the word "Confidential" in bold red font
            Aspose.Pdf.Facades.FormattedText formattedText = new Aspose.Pdf.Facades.FormattedText(
                "Confidential",                     // text
                System.Drawing.Color.Red,           // text color (System.Drawing.Color is required here)
                "Helvetica",                        // font name
                Aspose.Pdf.Facades.EncodingType.Winansi, // encoding
                true,                               // bold flag
                24);                                // font size

            // Bind the formatted text to the same stamp
            stamp.BindLogo(formattedText);

            // Position the combined stamp on the page
            stamp.SetOrigin(200f, 400f);   // X and Y coordinates
            stamp.IsBackground = false;   // render on top of page content
            stamp.Opacity = 0.7f;          // semi‑transparent

            // Apply the stamp to all pages of the document
            fileStamp.AddStamp(stamp);

            // Save the stamped PDF
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}