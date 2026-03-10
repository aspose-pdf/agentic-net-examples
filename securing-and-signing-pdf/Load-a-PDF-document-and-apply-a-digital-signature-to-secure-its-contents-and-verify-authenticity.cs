using System;
using System.IO;
using System.Drawing;                     // For System.Drawing.Rectangle
using Aspose.Pdf.Facades;                // PdfFileSignature
using Aspose.Pdf.Forms;                  // PKCS1, PKCS7, etc.

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";          // Source PDF
        const string signedPdf     = "signed.pdf";         // Output signed PDF
        const string certFile      = "certificate.pfx";    // PFX certificate
        const string certPassword  = "password";           // Certificate password
        const string appearanceImg = "signature_appearance.jpg"; // Optional appearance image

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certFile))
        {
            Console.Error.WriteLine($"Certificate file not found: {certFile}");
            return;
        }

        // -------------------------
        // Sign the PDF document
        // -------------------------
        using (PdfFileSignature signer = new PdfFileSignature())
        {
            // Load the PDF to be signed
            signer.BindPdf(inputPdf);

            // Provide the signing certificate
            signer.SetCertificate(certFile, certPassword);

            // Optional: set a visual appearance for the signature
            signer.SignatureAppearance = appearanceImg;

            // Define the signature rectangle (x, y, width, height)
            Rectangle rect = new Rectangle(100, 100, 200, 100);

            // Apply the digital signature on page 1
            signer.Sign(
                page: 1,
                SigReason: "Document approved",
                SigContact: "john.doe@example.com",
                SigLocation: "New York",
                visible: true,
                annotRect: rect);

            // Save the signed PDF
            signer.Save(signedPdf);
        }

        // -------------------------
        // Verify the applied signature
        // -------------------------
        using (PdfFileSignature verifier = new PdfFileSignature())
        {
            // Load the signed PDF
            verifier.BindPdf(signedPdf);

            // Check if any signature exists
            bool hasSignature = verifier.ContainsSignature();
            Console.WriteLine($"Contains signature: {hasSignature}");

            if (hasSignature)
            {
                // Retrieve all signature names (deep search)
                var signatureNames = verifier.GetSignatureNames(true);

                // Verify each signature individually
                foreach (var name in signatureNames)
                {
                    bool isValid = verifier.VerifySignature(name);
                    Console.WriteLine($"Signature '{name}' valid: {isValid}");
                }
            }
        }
    }
}