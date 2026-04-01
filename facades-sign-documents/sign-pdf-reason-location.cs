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
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the PDF to be signed
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPath);

        // Set the signing certificate
        pdfSignature.SetCertificate(certificatePath, certificatePassword);

        // Optional: set a visual appearance for the signature
        // pdfSignature.SignatureAppearance = "signature.png";

        // Define the rectangle where the visible signature will appear
        Rectangle rect = new Rectangle(100, 100, 200, 100);

        // Sign page 1 with reason and location
        pdfSignature.Sign(1, "Approved for release", "contact@example.com", "New York", true, rect);

        // Save the signed PDF
        pdfSignature.Save(outputPath);

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
