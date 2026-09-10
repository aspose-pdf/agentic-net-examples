using System;
using System.Drawing;               // System.Drawing.Rectangle is required by the Sign method
using Aspose.Pdf.Facades;          // PdfFileSignature facade
using Aspose.Pdf.Forms;            // PKCS1 signature class

class HideSignatureAppearance
{
    static void Main()
    {
        // Input PDF, output PDF and certificate details
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_hidden.pdf";
        const string certPath  = "certificate.pfx";
        const string certPwd   = "password";

        // Ensure the input files exist
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!System.IO.File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Create the PdfFileSignature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the PDF to be signed
            pdfSign.BindPdf(inputPdf);

            // Set the certificate that will be used for signing
            pdfSign.SetCertificate(certPath, certPwd);

            // Define a rectangle for the signature field (required even if invisible)
            // Position (100,100) with width=200 and height=100 (values are in points)
            Rectangle rect = new Rectangle(100, 100, 200, 100);

            // Sign the document:
            // - page 1 (Aspose.Pdf uses 1‑based indexing)
            // - reason, contact, location are optional strings
            // - visible = false hides the appearance completely
            // - rect is still needed but will not be rendered because visible is false
            pdfSign.Sign(
                page: 1,
                SigReason: "Document approved",
                SigContact: "john.doe@example.com",
                SigLocation: "New York",
                visible: false,
                annotRect: rect);

            // Save the signed PDF; the signature is cryptographically valid but invisible
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}' (signature appearance hidden).");
    }
}