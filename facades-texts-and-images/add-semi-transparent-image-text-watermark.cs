using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "watermarked.pdf";
        const string imagePath  = "logo.png";
        const string watermarkText = "CONFIDENTIAL";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // -----------------------------------------------------------------
        // Create a PdfFileStamp facade – it will edit the PDF and write the
        // result to the specified output file.
        // -----------------------------------------------------------------
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;   // source PDF
        fileStamp.OutputFile = outputPdf;  // destination PDF

        // Bind the source PDF to the facade
        fileStamp.BindPdf(inputPdf);

        // ---------------------------------------------------------------
        // 1) Image stamp – placed as background, semi‑transparent
        // ---------------------------------------------------------------
        Aspose.Pdf.Facades.Stamp imageStamp = new Aspose.Pdf.Facades.Stamp();
        imageStamp.BindImage(imagePath);          // use the image file
        imageStamp.IsBackground = true;          // render behind page content
        imageStamp.Opacity = 0.5f;                // 50 % opacity
        imageStamp.SetOrigin(100f, 400f);         // position (X, Y) in points

        // ---------------------------------------------------------------
        // 2) Text stamp – rendered over the image, also semi‑transparent
        // ---------------------------------------------------------------
        // FormattedText constructor sets text, color, font, encoding, bold flag, size
        Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
            watermarkText,
            System.Drawing.Color.Red,          // text color (System.Drawing.Color is required here)
            "Helvetica",                       // font name
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,                             // not bold
            48);                               // font size

        Aspose.Pdf.Facades.Stamp textStamp = new Aspose.Pdf.Facades.Stamp();
        textStamp.BindLogo(ft);                  // bind the formatted text
        textStamp.IsBackground = false;          // render on top of page content
        textStamp.Opacity = 0.5f;                // 50 % opacity for the text
        textStamp.SetOrigin(100f, 400f);         // same position as the image

        // ---------------------------------------------------------------
        // Add both stamps to the PDF
        // ---------------------------------------------------------------
        fileStamp.AddStamp(imageStamp);
        fileStamp.AddStamp(textStamp);

        // Persist changes and release resources
        fileStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}