using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string signedPdf = "signed.pdf";        // output signed PDF
        const string certPath = "certificate.pfx";   // PKCS#12 certificate
        const string certPassword = "password";      // certificate password

        // Verify input files exist
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

        // ---------- Sign the PDF ----------
        using (var signer = new PdfFileSignature())
        {
            signer.BindPdf(inputPdf);                       // load PDF to be signed
            signer.SetCertificate(certPath, certPassword); // set signing certificate

            // Define visible rectangle for the signature (x, y, width, height)
            Rectangle rect = new Rectangle(100, 100, 200, 100);

            // Sign on page 1 with reason, contact and location information
            signer.Sign(
                page: 1,
                SigReason: "Document signed for approval",
                SigContact: "contact@example.com",
                SigLocation: "Office",
                visible: true,
                annotRect: rect);

            // Save the signed PDF
            signer.Save(signedPdf);
            signer.Close();
        }

        // ---------- Verify the signature ----------
        using (var verifier = new PdfFileSignature())
        {
            verifier.BindPdf(signedPdf);                    // load the signed PDF

            // Retrieve all signature names (true = include empty fields, false = only filled)
            IList<SignatureName> signatureNames = verifier.GetSignatureNames(true);

            if (signatureNames.Count == 0)
            {
                Console.WriteLine("No signatures found in the document.");
            }
            else
            {
                // Verify each signature using the non‑obsolete API
                foreach (SignatureName sig in signatureNames)
                {
                    bool isValid = verifier.VerifySignature(sig);
                    Console.WriteLine($"Signature '{sig.Name}' valid: {isValid}");
                }
            }

            verifier.Close();
        }
    }
}
