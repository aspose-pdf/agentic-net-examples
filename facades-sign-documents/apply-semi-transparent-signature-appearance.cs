using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output signed PDF and the certificate file.
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certFile  = "certificate.pfx";
        const string certPwd   = "password";

        // Path to an image (PNG) that already contains a semi‑transparent background.
        // The image will be used as the visual appearance of the signature.
        const string signatureImage = "signature_appearance.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certFile))
        {
            Console.Error.WriteLine($"Certificate file not found: {certFile}");
            return;
        }
        if (!File.Exists(signatureImage))
        {
            Console.Error.WriteLine($"Signature appearance image not found: {signatureImage}");
            return;
        }

        // Create the PdfFileSignature facade.
        PdfFileSignature pdfSign = new PdfFileSignature();

        // Bind the source PDF.
        pdfSign.BindPdf(inputPdf);

        // Set the certificate that will be used for signing.
        pdfSign.SetCertificate(certFile, certPwd);

        // Assign the semi‑transparent image as the signature appearance.
        // The image should be a PNG with an alpha channel (e.g., 50% opacity).
        pdfSign.SignatureAppearance = signatureImage;

        // Define the rectangle where the signature will be placed (System.Drawing.Rectangle).
        // X, Y, Width, Height – coordinates are in points (1/72 inch).
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Sign the document on page 1 (pages are 1‑based).
        // Parameters: page, reason, contact, location, visible, rectangle.
        pdfSign.Sign(
            page: 1,
            SigReason: "Document approved",
            SigContact: "john.doe@example.com",
            SigLocation: "New York",
            visible: true,
            annotRect: rect);

        // Save the signed PDF.
        pdfSign.Save(outputPdf);

        // Close the facade (releases internal resources).
        pdfSign.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}