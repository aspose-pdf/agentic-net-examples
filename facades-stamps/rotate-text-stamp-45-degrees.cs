using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // FormattedText

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the facade and bind the source PDF (load)
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPath);

            // Create a text stamp (logo) with desired appearance
            FormattedText ft = new FormattedText(
                "CONFIDENTIAL",                 // text
                System.Drawing.Color.Red,       // text color (System.Drawing.Color required)
                "Helvetica",                    // font name
                EncodingType.Winansi,           // encoding
                false,                          // embed font flag
                36);                            // font size

            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(ft);

            // Rotate the stamp 45 degrees around its center
            stamp.Rotation = 45f; // arbitrary angle; alternatively stamp.RotateAngle = 45.0;

            // Position the stamp (example coordinates)
            stamp.SetOrigin(200, 400);

            // Add the stamp to the document (applies to all pages)
            fileStamp.AddStamp(stamp);

            // Save the modified PDF (save)
            fileStamp.Save(outputPath);
            fileStamp.Close();
        }

        Console.WriteLine($"Rotated text stamp saved to '{outputPath}'.");
    }
}