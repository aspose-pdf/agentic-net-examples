using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf      = "input.pdf";          // source PDF (already has a signature)
        const string outputPdf     = "output_signed.pdf"; // PDF after adding the second signature
        const string certificate   = "certificate.pfx";   // PKCS#12 certificate file
        const string certPassword  = "password";          // certificate password

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificate))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificate}");
            return;
        }

        // PdfFileSignature implements IDisposable, so wrap it in a using block
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the existing PDF document
            pdfSign.BindPdf(inputPdf);

            // Set the certificate that will be used for signing
            pdfSign.SetCertificate(certificate, certPassword);

            // Optional: set a visual appearance for the signature (e.g., an image)
            // pdfSign.SignatureAppearance = "signature_appearance.png";

            // Define the rectangle where the second signature will be placed on page 3
            // Rectangle(x, y, width, height) – coordinates are in points (1/72 inch)
            Rectangle secondSigRect = new Rectangle(100, 100, 200, 100);

            // Add the second digital signature
            pdfSign.Sign(
                page: 3,                     // page number (1‑based)
                SigReason: "Approved by second signer",
                SigContact: "second.signer@example.com",
                SigLocation: "New York",
                visible: true,               // make the signature visible
                annotRect: secondSigRect);   // rectangle defined above

            // Save the signed PDF to a new file
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Second digital signature added. Output saved to '{outputPdf}'.");
    }
}