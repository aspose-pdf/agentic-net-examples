using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
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

        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPath);
        pdfSignature.SetCertificate(certificatePath, certificatePassword);
        // Optional appearance image for the signature
        pdfSignature.SignatureAppearance = "signature_appearance.jpg";

        // First signature on page 2
        Rectangle rect1 = new Rectangle(100, 100, 200, 100);
        pdfSignature.Sign(2, "First Reason", "first@example.com", "LocationA", true, rect1);

        // Second signature on page 3 with a different rectangle area
        Rectangle rect2 = new Rectangle(300, 400, 150, 80);
        pdfSignature.Sign(3, "Second Reason", "second@example.com", "LocationB", true, rect2);

        pdfSignature.Save(outputPath);
        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}