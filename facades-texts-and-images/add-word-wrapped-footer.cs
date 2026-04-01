using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string footerText = "This is a sample footer that will wrap word by word across the page width.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the stamp facade and bind the source PDF
            PdfFileStamp stamp = new PdfFileStamp();
            stamp.BindPdf(inputPath);

            // Create formatted text with styling via constructor (no mutable properties)
            FormattedText formatted = new FormattedText(
                footerText,                     // text content
                Color.Black,                    // foreground color
                "Helvetica",                   // font name
                EncodingType.Winansi,           // encoding
                false,                          // embed font flag
                12);                            // font size (points)

            // Add the footer to every page with a bottom margin of 20 points
            stamp.AddFooter(formatted, 20f);

            // Save the result and release resources
            stamp.Save(outputPath);
            stamp.Close();

            Console.WriteLine($"Footer added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
