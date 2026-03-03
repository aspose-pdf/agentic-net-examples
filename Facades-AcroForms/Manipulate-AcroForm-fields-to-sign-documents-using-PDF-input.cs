using System;
using System.IO;
using Aspose.Pdf.Facades;      // PdfFileSignature
using Aspose.Pdf.Forms;       // PKCS1 signature class

class Program
{
    static void Main()
    {
        // Paths to the input PDF, output PDF and the signing certificate
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed.pdf";
        const string certPath  = "certificate.pfx";
        const string certPwd   = "password";

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

        // Create a PKCS#1 signature object and set its properties
        PKCS1 signature = new PKCS1(certPath, certPwd);
        signature.Reason      = "Document approved";
        signature.ContactInfo = "john.doe@example.com"; // <-- correct property name
        signature.Location    = "New York";

        // Define the visible signature rectangle (System.Drawing.Rectangle)
        // Parameters: x, y, width, height. Coordinates are in points.
        var rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Use the PdfFileSignature facade to apply the signature
        using (PdfFileSignature pdfSigner = new PdfFileSignature())
        {
            // Bind the source PDF
            pdfSigner.BindPdf(inputPdf);

            // Optional: set an image that will be used as the visual appearance
            // pdfSigner.SignatureAppearance = "signatureImage.png";

            // Sign on page 1 (Aspose.Pdf uses 1‑based page indexing)
            pdfSigner.Sign(
                page: 1,
                SigReason: signature.Reason,
                SigContact: signature.ContactInfo,
                SigLocation: signature.Location,
                visible: true,
                annotRect: rect,
                sig: signature);

            // Save the signed PDF
            pdfSigner.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}