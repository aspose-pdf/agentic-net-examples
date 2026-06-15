using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_output.pdf"; // result PDF
        const string certPath   = "certificate.pfx";   // signing certificate
        const string certPass   = "password";          // certificate password
        const string appearance = "signature.png";     // optional appearance image

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Use PdfFileSignature facade to add signatures
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the source PDF
            pdfSign.BindPdf(inputPdf);

            // Set certificate for signing
            pdfSign.SetCertificate(certPath, certPass);

            // Optional: set a graphic appearance for the signature
            if (File.Exists(appearance))
                pdfSign.SignatureAppearance = appearance;

            // First signature (example on page 2)
            Rectangle rect1 = new Rectangle(100, 100, 200, 100); // x, y, width, height
            pdfSign.Sign(
                page: 2,
                SigReason: "Approved",
                SigContact: "john.doe@example.com",
                SigLocation: "New York",
                visible: true,
                annotRect: rect1);

            // Second signature on page 3 with a different rectangle
            Rectangle rect2 = new Rectangle(300, 500, 180, 90);
            pdfSign.Sign(
                page: 3,
                SigReason: "Reviewed",
                SigContact: "jane.smith@example.com",
                SigLocation: "London",
                visible: true,
                annotRect: rect2);

            // Save the signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}