using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string footerText = "This is a sample footer text that will wrap word by word across the page width.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfFileStamp facade and bind the source PDF
        PdfFileStamp stamp = new PdfFileStamp();
        stamp.BindPdf(inputPath);

        // Create formatted text for the footer (color, font, encoding, embedded flag, font size)
        Aspose.Pdf.Facades.FormattedText formattedFooter = new Aspose.Pdf.Facades.FormattedText(
            footerText,
            System.Drawing.Color.Black,
            "Helvetica",
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            12);

        // Add the footer to every page with bottom, left and right margins (word‑by‑word wrapping is handled automatically)
        stamp.AddFooter(formattedFooter, 20f, 50f, 50f);

        // Save the modified PDF and release resources
        stamp.Save(outputPath);
        stamp.Close();

        Console.WriteLine($"Footer added and saved to '{outputPath}'.");
    }
}