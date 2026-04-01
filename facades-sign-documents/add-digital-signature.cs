using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the signature facade and bind the PDF
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPath);

        // Set the signing certificate
        pdfSignature.SetCertificate(certPath, certPassword);

        // Optional: set a visual appearance image for the signature
        // pdfSignature.SignatureAppearance = "signature.png";

        // Define the rectangle where the visible signature will appear
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Sign page 1 with reason and location
        pdfSignature.Sign(1, "Approved for release", "contact@example.com", "New York", true, rect);

        // Save the signed PDF
        pdfSignature.Save(outputPath);

        Console.WriteLine($"Signed PDF saved as '{outputPath}'.");
    }
}
