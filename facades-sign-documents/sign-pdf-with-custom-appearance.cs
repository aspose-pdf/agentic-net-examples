using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // PKCS7, SignatureCustomAppearance

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";
        const string appearanceImage = "signature.png";
        const int pageNumber = 1;

        // Signature rectangle (x, y, width, height)
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }
        if (!File.Exists(appearanceImage))
        {
            Console.Error.WriteLine($"Appearance image not found: {appearanceImage}");
            return;
        }

        // Bind PDF and configure signature
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPdf);
            pdfSign.SignatureAppearance = appearanceImage; // image used in appearance

            // Create PKCS7 signature object
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword);

            // Configure custom appearance: background color and foreground image
            SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
            customAppearance.BackgroundColor = Aspose.Pdf.Color.LightGray;
            customAppearance.IsForegroundImage = true; // draw image in foreground

            // Assign custom appearance to the signature
            pkcs7.CustomAppearance = customAppearance;

            // Sign the document
            pdfSign.Sign(pageNumber, "Document approved", "contact@example.com", "New York", true, rect, pkcs7);

            // Save signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}