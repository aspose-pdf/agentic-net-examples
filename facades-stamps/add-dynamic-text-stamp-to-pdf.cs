using System;
using System.IO;
using System.Drawing; // for Color
using Aspose.Pdf.Facades; // Facade classes (PdfFileStamp, Stamp, FormattedText, EncodingType)

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Dynamic data to embed in the stamp
        const string author = "John Doe";
        string stampText = $"Author: {author}, Date: {DateTime.Now:yyyy-MM-dd}";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // -------------------------------------------------
        // Initialize the PdfFileStamp facade and bind the PDF
        // -------------------------------------------------
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // -------------------------------------------------
        // Create a FormattedText object with the interpolated text
        // -------------------------------------------------
        FormattedText formattedText = new FormattedText(
            stampText,                     // text content
            Color.Blue,                    // foreground color (System.Drawing.Color)
            "Helvetica",                  // font name
            EncodingType.Winansi,          // encoding
            false,                         // embed the font?
            12);                           // font size

        // -------------------------------------------------
        // Create a Stamp, bind the FormattedText, and set appearance
        // -------------------------------------------------
        Stamp stamp = new Stamp();
        stamp.BindLogo(formattedText);
        stamp.SetOrigin(100, 700);   // X and Y position (from left & bottom)
        stamp.IsBackground = false; // place on top of page content
        stamp.Opacity = 0.7f;        // semi‑transparent

        // -------------------------------------------------
        // Add the stamp to the document (applies to all pages)
        // -------------------------------------------------
        fileStamp.AddStamp(stamp);

        // -------------------------------------------------
        // Save the stamped PDF and release resources
        // -------------------------------------------------
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
