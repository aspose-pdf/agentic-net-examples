using System;
using System.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfFileSignature facade, bind the PDF, set certificate, sign, and save.
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the source PDF file.
            pdfSign.BindPdf(inputPath);

            // Optional: set an image for the visual appearance of the signature.
            // pdfSign.SignatureAppearance = "signature_appearance.jpg";

            // Provide the certificate (PFX file) and its password.
            pdfSign.SetCertificate(certPath, certPassword);

            // Define the signature rectangle (x, y, width, height) on page 1.
            Rectangle rect = new Rectangle(100, 100, 200, 100);

            // Apply the digital signature to page 1.
            pdfSign.Sign(
                page: 1,
                SigReason: "Document approved",
                SigContact: "contact@example.com",
                SigLocation: "New York",
                visible: true,
                annotRect: rect
            );

            // Save the signed PDF to the output path.
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}