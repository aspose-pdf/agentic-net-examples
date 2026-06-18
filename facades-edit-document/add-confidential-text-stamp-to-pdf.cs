using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "confidential.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Create formatted text: red font, Helvetica, size 36
        FormattedText formattedText = new FormattedText(
            "Confidential",
            System.Drawing.Color.Red,          // text color
            "Helvetica",                       // font name
            EncodingType.Winansi,              // encoding
            false,                             // embed font
            36);                               // font size

        // Configure the stamp
        Stamp stamp = new Stamp();
        stamp.BindLogo(formattedText);   // set the text as stamp content
        stamp.IsBackground = true;       // render behind page content
        stamp.Opacity = 0.5f;            // semi‑transparent background
        stamp.SetOrigin(100f, 700f);     // optional position (X, Y)

        // Apply the stamp to all pages
        fileStamp.AddStamp(stamp);

        // Save the modified PDF and release resources
        fileStamp.Save(outputPath);
        fileStamp.Close();
    }
}