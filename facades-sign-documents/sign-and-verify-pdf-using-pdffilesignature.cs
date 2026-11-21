using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for input PDF, signed output, and the signing certificate (PFX)
        const string inputPdfPath = "input.pdf";
        const string signedPdfPath = "signed.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        // Validate required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // ---------- Sign the PDF ----------
        // Use Aspose.Pdf.Forms.PKCS7 to hold the signing credentials.
        PKCS7 pkcs = new PKCS7(certPath, certPassword)
        {
            Reason = "Document signed",
            Location = "Location"
        };

        // Optional: customise the visual appearance of the signature.
        // pkcs.CustomAppearance = new SignatureCustomAppearance
        // {
        //     BackgroundColor = Aspose.Pdf.Color.LightGray,
        //     IsForegroundImage = true
        // };

        // Create the facade that performs the signing.
        PdfFileSignature signer = new PdfFileSignature();
        signer.BindPdf(inputPdfPath);

        // Sign the PDF. The first argument is the name of the signature field.
        // If the field does not exist Aspose.PDF will create it on the first page.
        signer.Sign("Signature1", pkcs);

        // Save the signed PDF.
        signer.Save(signedPdfPath);

        // ---------- Verify the signature ----------
        PdfFileSignature verifier = new PdfFileSignature();
        verifier.BindPdf(signedPdfPath);

        // Verify the specific signature field we created.
        bool isSignatureValid = verifier.VerifySignature("Signature1");

        Console.WriteLine(isSignatureValid
            ? "Signature verification succeeded."
            : "Signature verification failed.");
    }
}
