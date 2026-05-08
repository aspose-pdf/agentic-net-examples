using System;
using System.IO;
using System.Drawing; // Fully qualified System.Drawing.Color is used
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize PdfFileStamp and bind the source PDF (new API)
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a stamp (facade) that will hold the watermark text
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Position the stamp (origin is measured from the lower‑left corner)
        stamp.SetOrigin(100, 400);          // X = 100, Y = 400
        stamp.Opacity = 0.5f;               // Semi‑transparent
        stamp.IsBackground = true;         // Draw behind page content

        // Build the watermark text with equal line spacing.
        // Use fully‑qualified System.Drawing.Color and a float for the font size.
        Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
            "CONFIDENTIAL",                 // First line text
            System.Drawing.Color.Red,        // Text color (fully qualified)
            "Helvetica",                    // Font name
            Aspose.Pdf.Facades.EncodingType.Winansi, // Encoding
            false,                           // Not embedded
            36f);                            // Font size (float)

        // Add additional lines with the same line height
        ft.AddNewLineText("DO NOT DISTRIBUTE");
        ft.AddNewLineText("FOR INTERNAL USE ONLY");

        // Bind the formatted text to the stamp
        stamp.BindLogo(ft);

        // Add the configured stamp to the PDF (applies to all pages)
        fileStamp.AddStamp(stamp);

        // Save the result using the new API
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
