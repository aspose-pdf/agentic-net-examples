using System;
using System.IO;
using System.Drawing;                     // Required for System.Drawing.Rectangle
using Aspose.Pdf.Facades;                // PdfFileSignature facade
using Aspose.Pdf.Forms;                  // PKCS1 signature class

class Program
{
    static void Main()
    {
        // Paths for the source PDF, signed output, and the signing certificate
        const string inputPdf   = "input.pdf";
        const string signedPdf  = "signed.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

        // Verify that required files exist
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

        // -----------------------------------------------------------------
        // 1. Create a PKCS#1 signature object using the certificate file
        // -----------------------------------------------------------------
        PKCS1 pkcs1 = new PKCS1(certPath, certPass);
        pkcs1.Reason      = "Document approval";
        pkcs1.ContactInfo = "john.doe@example.com";
        pkcs1.Location    = "New York";

        // -----------------------------------------------------------------
        // 2. Sign the PDF
        // -----------------------------------------------------------------
        PdfFileSignature signer = new PdfFileSignature();
        signer.BindPdf(inputPdf);                     // Load the PDF to be signed

        // Optional: set a graphic appearance for the visible signature
        // signer.SignatureAppearance = "signature_appearance.jpg";

        // Define the visible signature rectangle (System.Drawing.Rectangle)
        Rectangle rect = new Rectangle(100, 100, 200, 100);

        // Sign on page 1 with the name "Signature1"
        signer.Sign(
            page: 1,
            SigName: "Signature1",
            SigReason: pkcs1.Reason,
            SigContact: pkcs1.ContactInfo,
            SigLocation: pkcs1.Location,
            visible: true,
            annotRect: rect,
            sig: pkcs1);

        // Save the signed PDF
        signer.Save(signedPdf);
        signer.Close();

        Console.WriteLine($"Signed PDF saved to '{signedPdf}'.");

        // -----------------------------------------------------------------
        // 3. Verify the signature immediately after signing
        // -----------------------------------------------------------------
        PdfFileSignature verifier = new PdfFileSignature();
        verifier.BindPdf(signedPdf);                  // Load the signed PDF

        bool isValid = verifier.VerifySigned("Signature1");
        verifier.Close();

        Console.WriteLine($"Signature verification result: {(isValid ? "Valid" : "Invalid")}");
    }
}