using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_stamp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create formatted text for the stamp (FormatedText requires System.Drawing.Color)
        FormattedText formattedText = new FormattedText(
            "Rotated Stamp",          // text
            Color.Red,                // text color (System.Drawing.Color)
            "Helvetica",              // font name
            EncodingType.Winansi,     // encoding
            false,                    // embed font?
            36);                      // font size

        // Use PdfFileStamp facade to add the stamp
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF
            fileStamp.BindPdf(inputPath);

            // Create a generic stamp, bind the text, and set rotation to 90 degrees
            Stamp stamp = new Stamp();
            stamp.BindLogo(formattedText);
            stamp.Rotation = 90; // rotate 90 degrees clockwise

            // Add the stamp to all pages
            fileStamp.AddStamp(stamp);

            // Save the result
            fileStamp.Save(outputPath);
            fileStamp.Close();
        }

        // Simple verification output
        Console.WriteLine("Stamp rotation set to 90 degrees. The text should appear readable after rotation.");
    }
}