using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // for Rectangle

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string outputPdf     = "signed_output.pdf";
        const string certificate   = "certificate.pfx";
        const string certPassword  = "password";
        const string signatureImg  = "signature.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificate))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificate}");
            return;
        }

        // Create the signature facade and bind the source PDF
        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPdf);

        // Optional visual appearance for the signature
        pdfSign.SignatureAppearance = signatureImg;

        // Set the signing certificate
        pdfSign.SetCertificate(certificate, certPassword);

        // First signature (example on page 1)
        Rectangle rect1 = new Rectangle(100, 100, 200, 100);
        pdfSign.Sign(
            page: 1,
            SigReason: "First signature",
            SigContact: "first@example.com",
            SigLocation: "Location1",
            visible: true,
            annotRect: rect1);

        // Second signature on page 3 with a different rectangle
        Rectangle rect2 = new Rectangle(300, 400, 200, 100);
        pdfSign.Sign(
            page: 3,
            SigReason: "Second signature",
            SigContact: "second@example.com",
            SigLocation: "Location3",
            visible: true,
            annotRect: rect2);

        // Save the signed PDF
        pdfSign.Save(outputPdf);
        pdfSign.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}