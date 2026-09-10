using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfFileSignature
using Aspose.Pdf.Forms;           // PKCS1, SignatureCustomAppearance

class Program
{
    static void Main()
    {
        // Paths to the source PDF, output PDF and the signing certificate
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed.pdf";
        const string certPath  = "certificate.pfx";
        const string certPwd   = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a PKCS#1 signature object and assign a custom appearance
        PKCS1 signature = new PKCS1(certPath, certPwd);

        // Build a custom appearance that hides the default caption and other fields
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance
        {
            DigitalSignedLabel = "",   // hide "Digitally signed by" text
            ShowContactInfo    = false,
            ShowLocation       = false,
            ShowReason         = false
        };
        signature.CustomAppearance = customAppearance;

        // Define the rectangle where the signature will be placed.
        // PdfFileSignature expects a System.Drawing.Rectangle.
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Sign the document using the facade API.
        using (PdfFileSignature pdfSigner = new PdfFileSignature())
        {
            pdfSigner.BindPdf(inputPdf);          // load the PDF
            // Optional: set an image for the signature appearance
            // pdfSigner.SignatureAppearance = "signature.png";

            // Sign page 1, make the signature visible, using the custom appearance.
            pdfSigner.Sign(page: 1, visible: true, annotRect: rect, sig: signature);

            // Save the signed PDF.
            pdfSigner.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}