using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfFileSignature
using Aspose.Pdf.Forms;           // PKCS1 (if needed)

class Program
{
    static void Main()
    {
        const string inputPdf       = "input.pdf";          // source PDF
        const string outputPdf      = "output_signed.pdf";  // result PDF
        const string certificatePfx = "certificate.pfx";    // signing certificate
        const string certPassword   = "password";           // certificate password
        const string appearanceImg  = "signature.png";      // optional appearance image

        // Validate required files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificatePfx))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePfx}");
            return;
        }

        // Create the facade for PDF signing
        PdfFileSignature pdfSigner = new PdfFileSignature();

        // Load the PDF document into the facade
        pdfSigner.BindPdf(inputPdf);

        // Configure the certificate used for signing
        pdfSigner.SetCertificate(certificatePfx, certPassword);

        // Optional: set a graphic appearance for the signature
        pdfSigner.SignatureAppearance = appearanceImg;

        // Define a rectangle for the second signature on page 3.
        // System.Drawing.Rectangle(x, y, width, height) – (x,y) is the lower‑left corner.
        System.Drawing.Rectangle rectPage3 = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Add the second digital signature.
        pdfSigner.Sign(
            page: 3,
            SigReason: "Second signature",
            SigContact: "contact@example.com",
            SigLocation: "Office",
            visible: true,
            annotRect: rectPage3);

        // Save the signed PDF.
        pdfSigner.Save(outputPdf);

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}