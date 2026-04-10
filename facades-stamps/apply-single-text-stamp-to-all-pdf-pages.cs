using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf); // loads the document for stamping

        // Create a text stamp (logo) using FormattedText.
        // FormattedText constructor requires System.Drawing.Color for the text color.
        FormattedText ft = new FormattedText(
            "CONFIDENTIAL",               // text
            System.Drawing.Color.Red,    // text color
            "Helvetica",                 // font name
            EncodingType.Winansi,        // encoding
            false,                       // embed font
            48);                         // font size

        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(ft);               // set the stamp content
        stamp.IsBackground = true;        // place stamp behind page content
        stamp.Opacity = 0.5f;             // semi‑transparent
        stamp.SetOrigin(100, 500);        // position (X, Y) from lower‑left corner

        // By default, Pages = null, meaning the stamp will be applied to all pages.
        // Explicitly setting it to null makes the intention clear.
        stamp.Pages = null;

        // Add the stamp to the document. PdfFileStamp will apply it to every page efficiently.
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF.
        fileStamp.Save(outputPdf);
        fileStamp.Close(); // release resources

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}