using System;
using System.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed.pdf";
        const string pfxPath    = "certificate.pfx";
        const string pfxPassword = "password";

        // Ensure the input files exist before proceeding
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!System.IO.File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Use PdfFileSignature facade to sign the document
        using (PdfFileSignature pdfSigner = new PdfFileSignature())
        {
            // Bind the source PDF
            pdfSigner.BindPdf(inputPdf);

            // Provide the certificate and its password
            pdfSigner.SetCertificate(pfxPath, pfxPassword);

            // Optional: set a visual appearance for the signature (image file)
            // pdfSigner.SignatureAppearance = "signature_appearance.png";

            // Define the rectangle where the visible signature will appear
            // Rectangle(x, y, width, height) – coordinates are in points (1/72 inch)
            Rectangle signatureRect = new Rectangle(100, 100, 200, 100);

            // Sign page 1 with reason, contact, location, visibility flag, and rectangle
            pdfSigner.Sign(
                page: 1,
                SigReason: "Document approved",
                SigContact: "john.doe@example.com",
                SigLocation: "New York, USA",
                visible: true,
                annotRect: signatureRect);

            // Save the signed PDF
            pdfSigner.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdf}'.");
    }
}