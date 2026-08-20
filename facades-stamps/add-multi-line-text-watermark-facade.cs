using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color used by FormattedText
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace contains FormattedText, EncodingType, Stamp, PdfFileStamp

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "watermarked.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a FormattedText object for the watermark.
        // Constructor parameters: text, System.Drawing.Color, font name, EncodingType, isEmbedded, font size (float)
        Aspose.Pdf.Facades.FormattedText watermarkText = new Aspose.Pdf.Facades.FormattedText(
            "Confidential",                // first line
            System.Drawing.Color.Gray,      // text color (System.Drawing.Color)
            "Helvetica",                  // font name
            Aspose.Pdf.Facades.EncodingType.Winansi, // encoding
            false,                          // not embedded
            48f);                           // font size (float)

        // Add additional lines to the watermark
        watermarkText.AddNewLineText("Do Not Distribute");
        watermarkText.AddNewLineText("Company Internal Use Only");

        // Create a Stamp, bind the FormattedText, and configure it as a background watermark
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(watermarkText);
        stamp.IsBackground = true;   // place behind page content
        stamp.Opacity = 0.3f;        // semi‑transparent

        // Add the stamp to all pages of the document
        fileStamp.AddStamp(stamp);

        // Save the result
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
