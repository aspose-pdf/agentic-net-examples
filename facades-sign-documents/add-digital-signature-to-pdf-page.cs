using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

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

        // PdfFileSignature implements IDisposable; use using for deterministic disposal
        using (PdfFileSignature signer = new PdfFileSignature())
        {
            // Bind the source PDF document
            signer.BindPdf(inputPdf);

            // Define signature rectangle (coordinates in points)
            // System.Drawing.Rectangle expects (x, y, width, height) where (x, y) is the lower‑left corner.
            double llx = 100; // lower‑left X
            double lly = 100; // lower‑left Y
            double urx = 250; // upper‑right X
            double ury = 150; // upper‑right Y

            int rectX = (int)llx;
            int rectY = (int)lly;
            int rectWidth = (int)(urx - llx);
            int rectHeight = (int)(ury - lly);
            Rectangle signatureRect = new Rectangle(rectX, rectY, rectWidth, rectHeight);

            // Load the certificate (PKCS#7) that will be used for signing
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword);

            // Sign the PDF using the overload that accepts primitive parameters.
            // Parameters order: pageNumber, reason, contactInfo, location, isVisible, rectangle, pkcs7
            signer.Sign(
                1,                     // page number (1‑based)
                "Document signed",    // reason
                "contact@example.com", // contact info
                "Location",           // location
                true,                  // visible signature flag
                signatureRect,         // signature rectangle
                pkcs7);                // PKCS#7 certificate

            // Save the signed PDF to the desired output path
            signer.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
