using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // Create a simple source PDF so the example can run in an empty sandbox.
        // ---------------------------------------------------------------------
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Create a one‑page PDF and save it to the expected input path.
        using (Document seed = new Document())
        {
            seed.Pages.Add();
            seed.Save(inputPdf);
        }

        // ---------------------------------------------------------------
        // Initialize the PdfFileStamp facade using the modern API (BindPdf).
        // ---------------------------------------------------------------
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf); // replaces the obsolete InputFile property

        // ---------------------------------------------------------------
        // Build the stamp content.
        // ---------------------------------------------------------------
        // FormattedText constructor allows us to set text, color, font, encoding,
        // whether the text is bold/italic, and the font size in one call.
        Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
            "CONFIDENTIAL",
            System.Drawing.Color.Red,               // text color (fully qualified)
            "Helvetica",                           // font name
            Aspose.Pdf.Facades.EncodingType.Winansi, // encoding
            false,                                   // isBold (false)
            36);                                     // font size

        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(ft);
        // Ensure the stamp is drawn on top of existing page content.
        stamp.IsBackground = false;
        // Optional: position the stamp (origin is measured from the lower‑left corner).
        stamp.SetOrigin(100, 400);

        // Apply the stamp to all pages (default behaviour).
        fileStamp.AddStamp(stamp);

        // Save the stamped document using the modern API (Save).
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Stamp applied. Output saved to '{outputPdf}'.");
    }
}
