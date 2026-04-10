using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "watermarked.pdf";
        const string imagePath = "logo.png";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Input PDF or image file not found.");
            return;
        }

        // -------------------------------------------------
        // Step 1: Add the image as a background stamp
        // -------------------------------------------------
        PdfFileStamp imageStampFacade = new PdfFileStamp();
        imageStampFacade.BindPdf(inputPdf);

        Aspose.Pdf.Facades.Stamp imgStamp = new Aspose.Pdf.Facades.Stamp();
        imgStamp.BindImage(imagePath);
        imgStamp.IsBackground = true;               // place behind page content
        imgStamp.SetOrigin(100, 400);                // lower‑left corner of the image
        imgStamp.SetImageSize(200, 200);             // width and height

        imageStampFacade.AddStamp(imgStamp);

        // Save to a temporary PDF to later add the text stamp
        string tempPdf = Path.GetTempFileName();
        imageStampFacade.Save(tempPdf);
        imageStampFacade.Close();

        // -------------------------------------------------
        // Step 2: Add semi‑transparent text over the image
        // -------------------------------------------------
        PdfFileStamp textStampFacade = new PdfFileStamp();
        textStampFacade.BindPdf(tempPdf);

        Aspose.Pdf.Facades.Stamp txtStamp = new Aspose.Pdf.Facades.Stamp();

        // FormattedText constructor sets text, color, font, encoding, embed flag, size
        FormattedText ft = new FormattedText(
            watermarkText,
            System.Drawing.Color.Red,   // text color (System.Drawing.Color is required here)
            "Helvetica",
            EncodingType.Winansi,
            false,
            48);                         // font size

        txtStamp.BindLogo(ft);
        txtStamp.IsBackground = false;   // on top of page content
        txtStamp.Opacity = 0.5f;          // semi‑transparent
        txtStamp.SetOrigin(150, 500);     // position of the text

        textStampFacade.AddStamp(txtStamp);
        textStampFacade.Save(outputPdf);
        textStampFacade.Close();

        // Clean up the temporary file
        try { File.Delete(tempPdf); } catch { }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}