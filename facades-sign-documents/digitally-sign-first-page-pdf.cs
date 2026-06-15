using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath  = "certificate.pfx";
        const string certPassword = "password";

        // Verify input files exist
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

        // Create the PdfFileSignature facade, bind the source PDF
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPdf);

            // Optional: set a visible appearance image for the signature
            // pdfSign.SignatureAppearance = "signature_appearance.jpg";

            // Provide the certificate used for signing
            pdfSign.SetCertificate(certPath, certPassword);

            // Define the rectangle where the signature will appear on page 1
            // Rectangle(x, y, width, height) – coordinates are in points (1/72 inch)
            Rectangle sigRect = new Rectangle(100, 100, 200, 100);

            // Apply the digital signature to page 1
            pdfSign.Sign(
                page: 1,                     // page number (1‑based)
                SigReason: "Approved",       // reason for signing
                SigContact: "john.doe@example.com", // contact information
                SigLocation: "New York",     // location of signing
                visible: true,               // make the signature visible
                annotRect: sigRect);         // rectangle on the page

            // Save the signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}