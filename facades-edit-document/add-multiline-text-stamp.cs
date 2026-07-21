using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // ---------------------------------------------------------------------
        // Create a minimal source PDF if it does not already exist.
        // This makes the example self‑contained for the sandbox environment.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add();
                seed.Save(inputPdf);
            }
        }

        // Create the facade for stamping and bind the source PDF.
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdf);

            // Create a stamp object (facade type) – fully qualified to avoid ambiguity.
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Multiline text content.
            string multilineText = "First line\nSecond line\nThird line";

            // Build the formatted text: text, color, font, encoding, embed flag, size.
            Aspose.Pdf.Facades.FormattedText formatted = new Aspose.Pdf.Facades.FormattedText(
                multilineText,                 // text (lines separated by \n)
                System.Drawing.Color.Blue,    // text color (fully qualified to avoid ambiguity)
                "Arial",                     // font name
                Aspose.Pdf.Facades.EncodingType.Winansi, // encoding
                false,                        // embed font flag
                14);                          // font size

            // Bind the formatted text to the stamp.
            stamp.BindLogo(formatted);

            // Position the stamp on the page (X, Y coordinates from bottom‑left).
            stamp.SetOrigin(100, 500);

            // Ensure the stamp is drawn on top of page content.
            stamp.IsBackground = false;

            // Add the stamp to the PDF and save.
            fileStamp.AddStamp(stamp);
            fileStamp.Save(outputPdf);
        }

        Console.WriteLine($"Text stamp applied and saved to '{outputPdf}'.");
    }
}
