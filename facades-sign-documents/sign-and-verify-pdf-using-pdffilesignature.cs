using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the signed output, and the signing certificate (PFX)
        const string inputPdf = "input.pdf";
        const string signedPdf = "signed.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        // Verify that required files exist before proceeding
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

        try
        {
            // ------------------------------------------------------------
            // 1. Sign the PDF
            // ------------------------------------------------------------
            PdfFileSignature signer = new PdfFileSignature();
            signer.BindPdf(inputPdf);

            // Prepare the PKCS#1 certificate object (used for signing)
            PKCS1 pkcs1 = new PKCS1(certPath, certPassword);

            // Define a visible signature rectangle (System.Drawing.Rectangle)
            // (x, y, width, height) – coordinates are in points (1/72 inch)
            Rectangle signatureRect = new Rectangle(100, 100, 200, 50);

            // Sign the PDF on the first page. Use positional arguments because the
            // overload does not expose parameter names that can be used as named
            // arguments in the version of Aspose.Pdf.Facades referenced.
            signer.Sign(
                1,                     // page number (1‑based)
                "Document approved", // reason
                "",                   // contact info
                "Office",            // location
                true,                  // visible signature flag
                signatureRect,        // signature rectangle
                pkcs1);                // certificate

            // Save the signed document
            signer.Save(signedPdf);

            // ------------------------------------------------------------
            // 2. Verify the signature that was just added
            // ------------------------------------------------------------
            // Re‑bind the signer to the newly created signed PDF
            signer.BindPdf(signedPdf);

            var signatureNames = signer.GetSignatureNames(); // IList<SignatureName>
            bool isValid = false;
            if (signatureNames != null && signatureNames.Count > 0)
            {
                // Verify using the first (and only) signature name
                isValid = signer.VerifySignature(signatureNames[0]);
            }

            Console.WriteLine(isValid
                ? "Signature verification succeeded."
                : "Signature verification failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
