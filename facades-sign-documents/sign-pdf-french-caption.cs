using System;
using System.IO;
using System.Globalization;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // required for PKCS7 and SignatureCustomAppearance

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_french.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

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

        // Bind the PDF to the signature facade
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(inputPdf);

        // Create PKCS7 credentials using the certificate
        PKCS7 pkcs7 = new PKCS7(certPath, certPassword);
        pkcs7.Reason = "Document signé numériquement"; // French reason
        pkcs7.Location = "Paris";

        // Configure custom appearance with French locale (Reason/Location are set on PKCS7)
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance
        {
            Culture = new CultureInfo("fr-FR") // French locale for caption labels
        };
        pkcs7.CustomAppearance = customAppearance;

        // Define a visible rectangle for the signature on the first page (System.Drawing.Rectangle required)
        System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 100, 80);

        // Apply the signature to the first page and save the result
        pdfSigner.Sign(1, true, signatureRect, pkcs7);
        pdfSigner.Save(outputPdf);

        Console.WriteLine($"Signed PDF saved to '{outputPdf}' with French caption.");
    }
}
