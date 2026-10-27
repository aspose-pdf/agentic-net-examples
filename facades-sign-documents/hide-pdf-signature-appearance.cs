using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_hidden.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Bind the source PDF to the PdfFileSignature object.
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(inputPath);

        // Create a concrete PKCS7 signature (Signature is abstract).
        PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
        {
            Reason = "Document signed",
            Location = "Location",
            ContactInfo = string.Empty
        };

        // Zero‑size rectangle – makes the visual appearance invisible.
        // The rectangle type required by PdfFileSignature.Sign is System.Drawing.Rectangle.
        Rectangle invisibleRect = new Rectangle(0, 0, 0, 0);

        // Sign the first page (page index starts at 1). Set isVisible to false.
        // This creates a cryptographically valid signature with no visible appearance.
        pdfSigner.Sign(1, false, invisibleRect, pkcs7);

        // Save the signed PDF.
        pdfSigner.Save(outputPath);

        Console.WriteLine($"Signed PDF saved to '{outputPath}' with hidden signature appearance.");
    }
}
