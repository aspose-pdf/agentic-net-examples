using System;
using System.IO;
using System.Drawing; // System.Drawing.Color
using Aspose.Pdf; // Core PDF classes
using Aspose.Pdf.Facades; // PdfFileStamp, FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string footerText = "Confidential";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a FormattedText object for the footer.
        // Use System.Drawing.Color, specify font name, encoding, embed flag and a float font size.
        Aspose.Pdf.Facades.FormattedText footer = new Aspose.Pdf.Facades.FormattedText(
            footerText,
            System.Drawing.Color.Gray,          // Text color (System.Drawing.Color)
            "Helvetica",                       // Font name
            Aspose.Pdf.Facades.EncodingType.Winansi, // Encoding
            false,                               // Do not embed font
            12f);                                // Font size (float)

        // Initialize the PdfFileStamp facade.
        Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp();

        // Bind the source PDF.
        fileStamp.BindPdf(inputPath);

        // Add the footer 10 points above the bottom edge of each page.
        fileStamp.AddFooter(footer, 10f);

        // Save the stamped PDF.
        fileStamp.Save(outputPath);

        // Release resources.
        fileStamp.Close();

        Console.WriteLine($"Footer stamp added. Output saved to '{outputPath}'.");
    }
}
