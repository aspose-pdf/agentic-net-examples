using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // FontRepository, Font, FontTypes
using System.Drawing; // Color

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_branded.pdf";
        const string fontName = "Arial"; // system‑installed TrueType font
        const string stampText = "My Brand";

        // 1. Create a minimal source PDF because the sandbox has no files.
        using (Document seed = new Document())
        {
            seed.Pages.Add(); // add a blank page
            seed.Save(inputPdfPath);
        }

        // 2. Resolve the font name. FindFont returns a Font object; we only need the name.
        //    This avoids the overload that expects a Stream.
        Aspose.Pdf.Text.Font customFont = FontRepository.FindFont(fontName);

        // 3. Prepare the stamp using the loaded font.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdfPath);

        // FormattedText expects the font name; the font is already resolved above.
        Aspose.Pdf.Facades.FormattedText formattedText = new Aspose.Pdf.Facades.FormattedText(
            stampText,                     // text to display
            System.Drawing.Color.Black,    // text color
            fontName,                      // font name (must match the loaded font)
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,                         // do not embed via this flag (font already loaded)
            48f);                          // font size

        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formattedText);
        stamp.SetOrigin(100, 500);   // position (bottom‑left corner)
        stamp.Opacity = 0.5f;        // semi‑transparent
        stamp.IsBackground = true;   // render behind page content

        // 4. Apply the stamp and save the result.
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPdfPath);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}
