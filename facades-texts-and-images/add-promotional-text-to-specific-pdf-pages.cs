using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string promoMessage = "Buy our product now!";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Pages where the promotional message will be inserted (1‑based indexing)
        int[] targetPages = new int[] { 3, 5, 7 };

        // Position (lower‑left corner) where the text will appear on each page (points)
        double x = 100; // horizontal offset from the left
        double y = 500; // vertical offset from the bottom
        double rectWidth = 200;   // enough width for the message
        double rectHeight = 30;   // enough height for a single line
        double upperRightX = x + rectWidth;
        double upperRightY = y + rectHeight;

        // Build a FormattedText object from the Facades namespace.
        // The constructor expects System.Drawing.Color, a font name (optional),
        // an EncodingType, a bold flag, and the font size.
        FormattedText ft = new FormattedText(
            promoMessage,
            Color.Black,                 // System.Drawing.Color
            "Helvetica",               // font name (optional, can be null)
            EncodingType.Winansi,
            false,                       // isBold – already covered by the font name
            12f);                        // font size (float)

        // Use PdfFileMend – it provides an AddText overload that accepts an array of page numbers.
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPath);

        mend.AddText(
            ft,
            targetPages,
            (float)x,
            (float)y,
            (float)upperRightX,
            (float)upperRightY);

        mend.Save(outputPath);
        mend.Close();

        Console.WriteLine($"Promotional message added to pages {string.Join(", ", targetPages)}. Saved as '{outputPath}'.");
    }
}
