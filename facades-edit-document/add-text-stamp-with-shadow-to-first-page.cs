using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileStamp, Stamp, FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText  = "Sample Header";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade, bind the source PDF
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPath);

            // ---------- Shadow stamp (gray, slightly offset, semi‑transparent) ----------
            FormattedText shadowText = new FormattedText(
                stampText,
                System.Drawing.Color.Gray,   // shadow color
                "Helvetica",                 // font name
                EncodingType.Winansi,
                false,                       // not embedded
                12);                         // font size

            Stamp shadowStamp = new Stamp();
            shadowStamp.BindLogo(shadowText);
            // Position: a few points right/down from the main text to simulate a shadow
            shadowStamp.SetOrigin(102f, 792f);
            shadowStamp.Opacity = 0.5f;               // make it semi‑transparent
            shadowStamp.Pages = new int[] { 1 };      // apply only to page 1
            fileStamp.AddStamp(shadowStamp);

            // ---------- Main stamp (black, on top) ----------
            FormattedText mainText = new FormattedText(
                stampText,
                System.Drawing.Color.Black,  // main text color
                "Helvetica",
                EncodingType.Winansi,
                false,
                12);

            Stamp mainStamp = new Stamp();
            mainStamp.BindLogo(mainText);
            // Position at the top of page one (adjust Y as needed for your page size)
            mainStamp.SetOrigin(100f, 790f);
            mainStamp.Opacity = 1.0f;
            mainStamp.Pages = new int[] { 1 };
            fileStamp.AddStamp(mainStamp);

            // Save the stamped PDF
            fileStamp.Save(outputPath);
            fileStamp.Close();
        }

        Console.WriteLine($"Text stamp with shadow saved to '{outputPath}'.");
    }
}