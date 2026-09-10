using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Create the PdfFileSignature facade
        Aspose.Pdf.Facades.PdfFileSignature pdfSigner = new Aspose.Pdf.Facades.PdfFileSignature();

        // Load (bind) the PDF document to be signed
        pdfSigner.BindPdf(inputPath);

        // Set the signing certificate and its password
        pdfSigner.SetCertificate(certPath, certPassword);

        // Optional: set a visual appearance for the signature (image file path)
        // pdfSigner.SignatureAppearance = "signature_appearance.jpg";

        // Define the signature rectangle on page 1 (x, y, width, height)
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Add a visible digital signature to page 1
        pdfSigner.Sign(
            page: 1,
            SigReason: "Approved",
            SigContact: "john.doe@example.com",
            SigLocation: "New York",
            visible: true,
            annotRect: rect);

        // Save the signed PDF
        pdfSigner.Save(outputPath);

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}