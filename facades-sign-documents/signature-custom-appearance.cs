using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string signatureImage = "signature.png";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(signatureImage))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImage}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Define the signature rectangle (x, y, width, height) on the page.
        System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 500, 200, 100);

        // Create a custom appearance: background color and draw the image in the foreground.
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
        customAppearance.BackgroundColor = Aspose.Pdf.Color.LightGray;
        customAppearance.IsForegroundImage = true;

        // Create PKCS#7 signature and attach the custom appearance.
        PKCS7 pkcs7 = new PKCS7(certPath, certPassword);
        pkcs7.CustomAppearance = customAppearance;

        // Sign the PDF using PdfFileSignature.
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);
            pdfSignature.SignatureAppearance = signatureImage; // image used for the signature appearance
            pdfSignature.Sign(
                1, // page number (1‑based)
                "Document approved", // reason
                "John Doe", // contact info
                "New York, USA", // location
                true, // visible signature
                signatureRect,
                pkcs7);
            pdfSignature.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
