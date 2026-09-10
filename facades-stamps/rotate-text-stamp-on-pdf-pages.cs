using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileStamp, Stamp, FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_rotated_stamp.pdf";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a text stamp with formatted text
        // FormattedText constructor requires System.Drawing.Color for the text color
        FormattedText formatted = new FormattedText(
            "Rotated Stamp",                     // text
            System.Drawing.Color.Black,          // text color
            "Helvetica",                         // font name
            EncodingType.Winansi,                // encoding
            false,                               // embed font flag
            36);                                 // font size

        // Create the stamp and bind the formatted text
        Stamp stamp = new Stamp();
        stamp.BindLogo(formatted);

        // Set rotation to 90 degrees (clockwise)
        stamp.Rotation = 90f;

        // Position the stamp on the page (optional)
        stamp.SetOrigin(100f, 500f);

        // Add the stamp to all pages of the document
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        // Verification: output the rotation value that was set
        Console.WriteLine($"Stamp rotation set to: {stamp.Rotation} degrees");
        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}