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
        const string signatureImg = "signature.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Initialize the signature facade and bind the source PDF
        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPdf);

        // Set the certificate and optional appearance image
        pdfSign.SetCertificate(certPath, certPassword);
        pdfSign.SignatureAppearance = signatureImg;

        // First signature (example on page 2)
        Rectangle rect1 = new Rectangle(100, 100, 200, 100); // x, y, width, height
        pdfSign.Sign(2, "First signer", "first@example.com", "New York", true, rect1);

        // Second signature on page 3 with a different rectangle
        Rectangle rect2 = new Rectangle(300, 400, 150, 80); // x, y, width, height
        pdfSign.Sign(3, "Second signer", "second@example.com", "London", true, rect2);

        // Save the signed PDF
        pdfSign.Save(outputPdf);
        pdfSign.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}