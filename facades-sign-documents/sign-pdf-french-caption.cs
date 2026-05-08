using System;
using System.IO;
using System.Globalization;
using System.Drawing; // for Rectangle
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf; // for SignatureCustomAppearance

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_french.pdf";
        const string certificate = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificate))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificate}");
            return;
        }

        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the source PDF
            pdfSign.BindPdf(inputPdf);

            // Load the signing certificate
            pdfSign.SetCertificate(certificate, certPassword);

            // Create PKCS#1 signature object
            PKCS1 pkcs1 = new PKCS1(certificate, certPassword)
            {
                Reason = "Document approved",
                ContactInfo = "contact@example.com",
                Location = "Paris"
            };

            // Set French locale for the signature appearance caption
            pkcs1.CustomAppearance = new SignatureCustomAppearance
            {
                Culture = new CultureInfo("fr-FR")
            };

            // The Sign overload expects System.Drawing.Rectangle, not Aspose.Pdf.Rectangle
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Sign page 1 with visible signature using the overload that takes positional arguments
            pdfSign.Sign(
                1,                     // page number (1‑based)
                "Document approved", // reason
                "contact@example.com", // contact info
                "Paris",               // location
                true,                   // visible signature flag
                rect,                   // signature rectangle
                pkcs1);                 // PKCS#1 signature object

            // Save the signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
