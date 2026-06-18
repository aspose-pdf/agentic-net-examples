using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output signed PDF, and certificate details
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath  = "certificate.pfx";
        const string certPassword = "password";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Create a signature object (PKCS1) and set reason and location
        PKCS1 signature = new PKCS1(certPath, certPassword);
        signature.Reason   = "Approved for release";
        signature.Location = "New York";
        // Contact information can be left empty if not needed
        signature.ContactInfo = string.Empty;

        // Define the rectangle where the visible signature will appear
        // (x, y, width, height) – coordinates are in points (1/72 inch)
        Rectangle annotRect = new Rectangle(100, 100, 200, 50);

        // Use PdfFileSignature facade to apply the signature
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the source PDF
            pdfSign.BindPdf(inputPdf);

            // Optional: set an image appearance for the signature
            // pdfSign.SignatureAppearance = "signature_image.png";

            // Sign the document:
            // page = 1 (first page, 1‑based indexing)
            // visible = true (signature will be shown)
            // annotRect = rectangle defined above
            // signature = PKCS1 object with Reason and Location set
            pdfSign.Sign(1, true, annotRect, signature);

            // Save the signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}