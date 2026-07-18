using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for input and output PDFs
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a PdfFileStamp facade (does NOT implement IDisposable)
        PdfFileStamp fileStamp = new PdfFileStamp();

        // Bind the source PDF file
        fileStamp.BindPdf(inputPdf);

        // Create a text stamp using FormattedText
        // Constructor: FormattedText(string text, System.Drawing.Color color,
        //                           string fontName, EncodingType encoding, bool isEmbedded, double fontSize)
        FormattedText ft = new FormattedText(
            "CONFIDENTIAL",                     // stamp text
            System.Drawing.Color.Red,           // text color
            "Helvetica",                        // font name
            EncodingType.Winansi,               // encoding
            false,                              // embed font?
            48);                                // font size

        // Initialize the stamp and bind the formatted text
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(ft);                     // set the text as stamp content

        // Set stamp properties
        stamp.Opacity = 0.7f;                   // 70% opacity (0.0 to 1.0)
        stamp.IsBackground = true;              // place stamp behind page content
        stamp.Pages = new int[] { 1, 3, 5 };     // apply only to selected pages (example)

        // Add the stamp to the document
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF
        fileStamp.Save(outputPdf);

        // Close the facade (releases internal resources)
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}