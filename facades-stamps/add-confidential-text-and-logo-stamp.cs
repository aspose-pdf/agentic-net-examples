using System;
using System.IO;
using Aspose.Pdf.Facades; // Stamp, FormattedText, EncodingType
using System.Drawing; // for Color

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPath = "logo.png";

        // Verify files exist
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

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf); // loads the document to be stamped

        // Create formatted text for the word "Confidential" in bold
        FormattedText confidentialText = new FormattedText(
            "Confidential",          // text
            Color.Red,                // text color (System.Drawing.Color)
            "Helvetica-Bold",       // bold font name
            EncodingType.Winansi,    // encoding
            false,                    // embed font?
            36);                      // font size

        // Create a Stamp and bind both the text and the logo image
        Stamp stamp = new Stamp();
        stamp.BindLogo(confidentialText); // bind the formatted text
        stamp.BindImage(logoPath);        // bind the logo image

        // Position the stamp (origin is the lower‑left corner of the stamp)
        stamp.SetOrigin(100f, 500f); // X, Y in points

        // Optionally set the size of the image part of the stamp
        stamp.SetImageSize(80f, 80f); // width, height in points

        // Make the stamp appear behind the page content (optional)
        stamp.IsBackground = true;

        // Add the stamp to the PDF
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF
        fileStamp.Save(outputPdf);

        // Close the facade (releases resources)
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
