using System;
using System.IO;
using System.Drawing;               // Needed for Rectangle
using Aspose.Pdf.Facades;          // Facade classes for signing

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the output PDF and the new certificate
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string certFile   = "newcert.pfx";
        const string certPass   = "password";

        // Verify that required files exist
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

        // Create the PdfFileSignature facade
        PdfFileSignature signer = new PdfFileSignature();

        // Bind the PDF file to the facade
        signer.BindPdf(inputPdf);

        // Remove any existing signatures
        signer.RemoveSignatures();

        // Set the new certificate (PFX file) and its password
        signer.SetCertificate(certFile, certPass);

        // Optional: set a visual appearance for the signature (e.g., an image)
        // signer.SignatureAppearance = "signature_appearance.png";

        // Define the rectangle where the signature will appear (x, y, width, height)
        // Coordinates are in points; (0,0) is the lower‑left corner of the page.
        Rectangle signatureRect = new Rectangle(100, 100, 200, 50);

        // Sign the document.
        // Page numbers are 1‑based. The signature will be visible and placed
        // at the rectangle defined above.
        signer.Sign(
            page: 1,
            SigReason: "Updated signature",
            SigContact: "contact@example.com",
            SigLocation: "New York",
            visible: true,
            annotRect: signatureRect);

        // Save the signed PDF to the output path
        signer.Save(outputPdf);

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}