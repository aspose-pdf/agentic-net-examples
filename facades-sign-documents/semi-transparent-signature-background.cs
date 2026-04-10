using System;
using System.Drawing;                     // for System.Drawing.Rectangle
using Aspose.Pdf;                         // for Aspose.Pdf.Color
using Aspose.Pdf.Facades;                 // PdfFileSignature, PKCS1
using Aspose.Pdf.Forms;                   // SignatureCustomAppearance

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

        // Verify files exist (optional safety check)
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!System.IO.File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Define signature rectangle (x, y, width, height) in points
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 80);

        // Create a semi‑transparent background color (e.g., 50% opaque white)
        Aspose.Pdf.Color semiTransparentWhite = Aspose.Pdf.Color.FromArgb(128, 255, 255, 255);

        // Prepare custom appearance with the background color
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
        customAppearance.BackgroundColor = semiTransparentWhite;

        // Create the PKCS#1 signature object and attach the custom appearance
        PKCS1 pkcs1Signature = new PKCS1(certPath, certPass);
        pkcs1Signature.CustomAppearance = customAppearance;

        // Optional: set other signature metadata
        pkcs1Signature.Reason   = "Document approved";
        pkcs1Signature.ContactInfo = "contact@example.com";
        pkcs1Signature.Location = "New York, USA";

        // Sign the PDF using Aspose.Pdf.Facades
        using (PdfFileSignature pdfSigner = new PdfFileSignature())
        {
            pdfSigner.BindPdf(inputPdf);                     // load source PDF
            // If you have an image to use as the visual signature, set it here:
            // pdfSigner.SignatureAppearance = "signature_image.jpg";

            // Sign on page 1 (pages are 1‑based), make the signature visible
            pdfSigner.Sign(
                page: 1,
                SigReason: pkcs1Signature.Reason,
                SigContact: pkcs1Signature.ContactInfo,
                SigLocation: pkcs1Signature.Location,
                visible: true,
                annotRect: rect,
                sig: pkcs1Signature);

            pdfSigner.Save(outputPdf);                       // write signed PDF
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}