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

        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPath);
        pdfSign.SetCertificate(certPath, certPassword);
        // Optional: set an image to appear as the signature appearance
        // pdfSign.SignatureAppearance = "signature.png";

        System.Drawing.Rectangle rectFirst = new System.Drawing.Rectangle(100, 100, 200, 100);
        pdfSign.Sign(2, "First signature", "alice@example.com", "New York", true, rectFirst);

        System.Drawing.Rectangle rectSecond = new System.Drawing.Rectangle(300, 400, 150, 80);
        pdfSign.Sign(3, "Second signature", "bob@example.com", "London", true, rectSecond);

        pdfSign.Save(outputPath);
        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
