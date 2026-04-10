using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

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

        // Initialize the PdfFileSignature facade
        PdfFileSignature signer = new PdfFileSignature();

        // Bind the source PDF document
        signer.BindPdf(inputPdf);

        // Set the certificate (PFX) and its password
        signer.SetCertificate(certPath, certPassword);

        // Optional: set a visible appearance image for the signature
        // signer.SignatureAppearance = "signature_appearance.jpg";

        // Define the signature rectangle (x, y, width, height)
        Rectangle rect = new Rectangle(100, 100, 200, 100);

        // Apply the digital signature on page 1
        signer.Sign(
            page: 1,
            SigReason: "Approved",
            SigContact: "john.doe@example.com",
            SigLocation: "New York",
            visible: true,
            annotRect: rect);

        // Save the signed PDF
        signer.Save(outputPdf);

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}