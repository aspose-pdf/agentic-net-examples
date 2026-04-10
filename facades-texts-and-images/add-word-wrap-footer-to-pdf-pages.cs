using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Footer text that should wrap word‑by‑word.
        const string footerText = "This is a sample footer text that should wrap word by word across the page width.";

        // Margins control where the footer is placed and how wide the text area is.
        const float bottomMargin = 20f;   // distance from the bottom edge
        const float leftMargin   = 50f;   // left inset for wrapping
        const float rightMargin  = 50f;   // right inset for wrapping

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a FormattedText object with desired styling.
        // Constructor: (text, System.Drawing.Color, fontName, EncodingType, isEmbedded, fontSize)
        FormattedText formattedFooter = new FormattedText(
            footerText,
            System.Drawing.Color.DarkGray,
            "Helvetica",
            EncodingType.Winansi,
            false,
            10);

        // Use PdfFileStamp (Facade) to add the footer to every page.
        using (PdfFileStamp stamp = new PdfFileStamp())
        {
            // Load the source PDF.
            stamp.BindPdf(inputPath);

            // Add the footer. The left/right margins cause the text to wrap automatically.
            stamp.AddFooter(formattedFooter, bottomMargin, leftMargin, rightMargin);

            // Save the modified document.
            stamp.Save(outputPath);

            // Close releases internal resources (optional because of using).
            stamp.Close();
        }

        Console.WriteLine($"Footer added and saved to '{outputPath}'.");
    }
}