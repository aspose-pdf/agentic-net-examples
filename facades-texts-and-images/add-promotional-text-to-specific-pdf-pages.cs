using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API (PdfFileStamp, Stamp)
using Aspose.Pdf.Text;      // FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string promoText  = "Special Offer: 20% OFF!";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create formatted text for the promotional message.
        // FormattedText constructor requires System.Drawing.Color for the text color.
        FormattedText formatted = new FormattedText(
            promoText,                     // text
            System.Drawing.Color.Red,      // text color
            "Helvetica",                   // font name
            EncodingType.Winansi,          // encoding
            false,                         // embed font?
            24);                           // font size

        // Create a stamp, bind the formatted text, and restrict it to pages 3, 5 and 7.
        Stamp stamp = new Stamp();
        stamp.BindLogo(formatted);
        stamp.Pages = new int[] { 3, 5, 7 };   // 1‑based page numbers

        // Apply the stamp to the PDF using PdfFileStamp.
        using (PdfFileStamp fileStamp = new PdfFileStamp(inputPath, outputPath))
        {
            fileStamp.AddStamp(stamp);
            fileStamp.Close();   // finalize and write the output file
        }

        Console.WriteLine($"Promotional message added to pages 3, 5, and 7. Saved as '{outputPath}'.");
    }
}