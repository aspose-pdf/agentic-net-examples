using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "watermarked.pdf";
        const string imagePath  = "logo.png";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {imagePath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a stamp that contains both an image and semi‑transparent text
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind the image to the stamp
        stamp.BindImage(imagePath);

        // Create formatted text (System.Drawing.Color is required by FormattedText)
        FormattedText ft = new FormattedText(
            watermarkText,                     // text
            System.Drawing.Color.Red,          // text color
            "Helvetica",                       // font name
            EncodingType.Winansi,              // encoding
            false,                             // embed font
            48);                               // font size

        // Bind the text to the same stamp
        stamp.BindLogo(ft);

        // Position the stamp (center of the page, adjust as needed)
        stamp.SetOrigin(200, 400); // X, Y coordinates

        // Make the stamp appear behind existing content and set transparency
        stamp.IsBackground = true;
        stamp.Opacity = 0.5f; // 50 % transparent

        // Add the stamp to all pages of the document
        fileStamp.AddStamp(stamp);

        // Save the result
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}