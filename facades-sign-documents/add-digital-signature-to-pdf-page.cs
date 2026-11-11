using System;
using System.IO;
using System.Drawing; // for System.Drawing.Rectangle
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // for PKCS1

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string outputPdf     = "signed_output.pdf";
        const string certificate   = "certificate.pfx";
        const string certPassword  = "password";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificate))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificate}");
            return;
        }

        // PdfFileSignature implements IDisposable – use a using block for deterministic cleanup
        using (PdfFileSignature signature = new PdfFileSignature())
        {
            // Bind the source PDF document to the signature object
            signature.BindPdf(inputPdf);

            // Define the visible signature rectangle on page 1 (lower‑left and upper‑right coordinates)
            double llx = 100; // lower‑left X
            double lly = 100; // lower‑left Y
            double urx = 300; // upper‑right X
            double ury = 200; // upper‑right Y

            // Convert to System.Drawing.Rectangle (x, y, width, height)
            Rectangle rect = new Rectangle(
                (int)llx,
                (int)lly,
                (int)(urx - llx),
                (int)(ury - lly)
            );

            // Create the PKCS1 signature object from the certificate
            PKCS1 pkcs = new PKCS1(certificate, certPassword);

            // Add the digital signature to page 1
            // Parameters: pageNumber, reason, contactInfo, location, isVisible, rectangle, signatureObject
            signature.Sign(
                1,
                "Document signed",
                "contact@example.com",
                "Location",
                true,
                rect,
                pkcs
            );

            // Save the signed PDF to the desired output path
            signature.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
