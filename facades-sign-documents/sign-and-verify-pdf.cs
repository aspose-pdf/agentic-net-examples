using System;
using System.IO;
using System.Drawing;               // needed for Rectangle
using Aspose.Pdf.Facades;          // PdfFileSignature and related classes

class Program
{
    static void Main()
    {
        // Input PDF, output signed PDF, certificate (PFX) and its password
        const string inputPdf   = "input.pdf";
        const string signedPdf  = "signed_output.pdf";
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

        // ---------- Sign the PDF ----------
        // Create the facade, bind the source PDF and configure signing
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(inputPdf);                     // load PDF
        pdfSigner.SetCertificate(certPath, certPass);   // set signing certificate

        // Optional: visual appearance of the signature (image file)
        // pdfSigner.SignatureAppearance = "signature_image.jpg";

        // Define the rectangle where the visible signature will be placed
        // (x, y, width, height) – coordinates are in points (1/72 inch)
        Rectangle sigRect = new Rectangle(100, 100, 200, 100);

        // Perform the signing on page 1
        pdfSigner.Sign(
            page: 1,
            SigReason: "Document approved",
            SigContact: "john.doe@example.com",
            SigLocation: "New York",
            visible: true,
            annotRect: sigRect);

        // Save the signed PDF
        pdfSigner.Save(signedPdf);
        Console.WriteLine($"PDF signed and saved to '{signedPdf}'.");

        // ---------- Verify the signature ----------
        // Retrieve the list of signature field names (true = only filled signatures)
        var signatureNames = pdfSigner.GetSignatureNames(true);
        if (signatureNames.Count == 0)
        {
            Console.WriteLine("No signatures found to verify.");
            return;
        }

        // The collection returns objects of type SignatureName – extract the string name
        string firstSigName = signatureNames[0].Name; // <-- corrected conversion

        // Use the non‑obsolete VerifySignature method
        bool isValid = pdfSigner.VerifySignature(firstSigName);
        Console.WriteLine($"Signature '{firstSigName}' verification result: {isValid}");
    }
}
