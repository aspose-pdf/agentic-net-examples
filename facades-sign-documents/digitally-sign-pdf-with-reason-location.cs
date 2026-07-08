using System;
using System.IO;
using System.Drawing;               // Rectangle is defined here
using Aspose.Pdf.Facades;          // PdfFileSignature resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "signed_output.pdf"; // signed PDF destination
        const string certPath   = "certificate.pfx";   // signing certificate
        const string certPwd    = "password";          // certificate password

        // Verify that the input PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileSignature implements IDisposable, so wrap it in a using block
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the PDF to be signed
            pdfSign.BindPdf(inputPath);

            // Provide the certificate required for digital signing
            pdfSign.SetCertificate(certPath, certPwd);

            // Optional: set a graphic appearance for the signature
            // pdfSign.SignatureAppearance = "signature_image.png";

            // Define the rectangle where the visible signature will appear
            // Parameters: x, y, width, height (all in points)
            Rectangle rect = new Rectangle(100, 100, 200, 100);

            // Sign page 1 with the required reason and location
            // SigContact is left empty in this example
            pdfSign.Sign(
                page: 1,
                SigReason: "Approved for release",
                SigContact: "",
                SigLocation: "New York",
                visible: true,
                annotRect: rect);

            // Save the signed PDF to the output path
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}