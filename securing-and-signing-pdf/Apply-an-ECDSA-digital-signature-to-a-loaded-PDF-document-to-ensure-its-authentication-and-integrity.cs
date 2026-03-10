using System;
using System.IO;
using System.Drawing;                 // Required for System.Drawing.Rectangle
using Aspose.Pdf.Facades;            // PdfFileSignature facade
using Aspose.Pdf.Forms;              // PKCS7 (ECDSA capable) signature class

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";               // Source PDF
        const string outputPdf     = "signed_output.pdf";       // Signed PDF
        const string certificatePfx = "ecdsa_certificate.pfx"; // PFX containing ECDSA key
        const string certificatePwd = "password";               // PFX password

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificatePfx))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePfx}");
            return;
        }

        // Create the PdfFileSignature facade and bind the source document
        using (PdfFileSignature pdfSigner = new PdfFileSignature())
        {
            pdfSigner.BindPdf(inputPdf);

            // Associate the ECDSA certificate with the signing routine
            pdfSigner.SetCertificate(certificatePfx, certificatePwd);

            // Optional: set a visible appearance image for the signature
            // pdfSigner.SignatureAppearance = "signature_appearance.png";

            // Define the signature rectangle (x, y, width, height) on page 1
            Rectangle signatureRect = new Rectangle(100, 100, 200, 100);

            // Apply the digital signature on page 1
            pdfSigner.Sign(
                page: 1,
                SigReason: "Document approved",
                SigContact: "john.doe@example.com",
                SigLocation: "New York, USA",
                visible: true,
                annotRect: signatureRect);

            // Persist the signed PDF
            pdfSigner.Save(outputPdf);
        }

        Console.WriteLine($"PDF successfully signed and saved to '{outputPdf}'.");
    }
}