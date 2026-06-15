using System;
using System.IO;
using System.Drawing;               // For Rectangle (visible signature bounds)
using Aspose.Pdf.Facades;          // PdfFileSignature facade
using Aspose.Pdf.Forms;            // PKCS1 / PKCS7 signature classes (if needed)

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // Source PDF to be signed
        const string outputPdf  = "signed_output.pdf"; // Destination signed PDF
        const string pfxPath    = "certificate.pfx";   // PFX certificate file
        const string pfxPassword = "password";         // Certificate password

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Initialize the signing facade and bind the PDF
        using (PdfFileSignature signer = new PdfFileSignature())
        {
            signer.BindPdf(inputPdf);

            // Optional: set an image to appear as the signature appearance
            // signer.SignatureAppearance = "signature_image.jpg";

            // Provide the certificate and its password
            signer.SetCertificate(pfxPath, pfxPassword);

            // Define the rectangle where the visible signature will be placed
            // Rectangle(x, y, width, height) – coordinates are in points
            Rectangle rect = new Rectangle(100, 100, 200, 100);

            // Sign the document on page 1 with visible signature
            signer.Sign(
                page: 1,
                SigReason: "Approved",
                SigContact: "John Doe",
                SigLocation: "New York",
                visible: true,
                annotRect: rect);

            // Save the signed PDF
            signer.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}