using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for FontRepository

class Program
{
    static void Main()
    {
        const string inputPdf = "product.pdf";
        const string outputPdf = "product_verified.pdf";
        const string qrImage = "qr.png";
        const string description = "Product Verification";

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(qrImage))
        {
            Console.Error.WriteLine("Input PDF or QR image not found.");
            return;
        }

        // ---------- QR CODE IMAGE STAMP ----------
        // Use the Facades Stamp class (fully qualified) to avoid ambiguity.
        Aspose.Pdf.Facades.Stamp imageStamp = new Aspose.Pdf.Facades.Stamp();
        imageStamp.BindImage(qrImage);
        imageStamp.SetOrigin(100, 500);          // position (x, y) from bottom‑left
        imageStamp.SetImageSize(100, 100);       // size of the image stamp
        imageStamp.Opacity = 0.9f;               // semi‑transparent
        imageStamp.IsBackground = false;        // draw on top of page content

        // ---------- TEXT STAMP ----------
        // FormattedText lives in the Facades namespace; use font name string, not FontStyle enum.
        Aspose.Pdf.Facades.FormattedText txt = new Aspose.Pdf.Facades.FormattedText(
            description,
            System.Drawing.Color.Black,               // System.Drawing.Color is accepted
            "Helvetica",                            // font name
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            12);

        Aspose.Pdf.Facades.Stamp textStamp = new Aspose.Pdf.Facades.Stamp();
        textStamp.BindLogo(txt);
        textStamp.SetOrigin(210, 540);            // place to the right of the QR code
        textStamp.IsBackground = false;
        textStamp.Opacity = 0.9f;

        // ---------- APPLY STAMPS ----------
        Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp();
        fileStamp.BindPdf(inputPdf);
        fileStamp.AddStamp(imageStamp);
        fileStamp.AddStamp(textStamp);
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
