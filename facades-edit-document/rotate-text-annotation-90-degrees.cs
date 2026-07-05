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
        const string outputPath = "output_rotated_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to ensure proper resource handling (document disposal rule)
        using (Document doc = new Document(inputPath))
        {
            // No direct modifications needed here; the facade will handle stamping.
        }

        // Create a text stamp that will serve as the rotated annotation
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // FormattedText constructor sets text, color, font, encoding, embed flag, and size
        FormattedText formattedText = new FormattedText(
            "Diagonal Text",                     // text content
            System.Drawing.Color.Black,          // text color (System.Drawing.Color is required here)
            "Helvetica",                         // font name
            EncodingType.Winansi,                // encoding
            false,                               // embed font flag
            24);                                 // font size

        // Bind the formatted text to the stamp
        stamp.BindLogo(formattedText);

        // Rotate the stamp 90 degrees to align with diagonal content
        stamp.Rotation = 90;

        // Apply the stamp using the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);   // load source PDF
        fileStamp.AddStamp(stamp);      // add the rotated text stamp
        fileStamp.Save(outputPath);     // save the result
        fileStamp.Close();              // close the facade

        Console.WriteLine($"Rotated text annotation saved to '{outputPath}'.");
    }
}