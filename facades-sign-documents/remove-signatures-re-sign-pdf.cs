using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_updated.pdf";
        const string certPath   = "new_certificate.pfx";
        const string certPass   = "certPassword";

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

        // Use PdfFileSignature facade to manipulate signatures
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the existing PDF
            pdfSign.BindPdf(inputPdf);

            // Remove all existing signatures (if any)
            pdfSign.RemoveSignatures();

            // Set the new certificate for signing
            pdfSign.SetCertificate(certPath, certPass);

            // Optional: set a visual appearance for the signature
            // pdfSign.SignatureAppearance = "signature_image.png";

            // Define the rectangle where the signature will appear (page coordinates)
            Rectangle signatureRect = new Rectangle(100, 100, 200, 50); // x, y, width, height

            // Sign the document on page 1 (pages are 1‑based)
            pdfSign.Sign(
                page: 1,
                SigReason: "Document updated",
                SigContact: "contact@example.com",
                SigLocation: "New York",
                visible: true,
                annotRect: signatureRect
            );

            // Save the newly signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPdf}'.");
    }
}