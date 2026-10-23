using System;
using System.IO;
using System.Globalization;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // PKCS7 and SignatureCustomAppearance are in this namespace for the used version

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "signed_fr.pdf";      // signed PDF with French caption
        const string certPath   = "certificate.pfx";    // digital certificate file
        const string certPwd    = "password";           // certificate password

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Bind the PDF to the signature facade
        PdfFileSignature signer = new PdfFileSignature();
        signer.BindPdf(inputPath);

        // Create a PKCS7 signature object using the certificate
        PKCS7 pkcs7 = new PKCS7(certPath, certPwd)
        {
            ContactInfo = "Contact",
            // LocationInfo property is not available in this version of Aspose.Pdf, so it is omitted.
            Reason = "Approved"
        };

        // Configure custom appearance and set the locale to French
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance
        {
            Culture = new CultureInfo("fr-FR") // forces the digital‑signed caption to French
        };
        pkcs7.CustomAppearance = customAppearance;

        // Apply the signature and save the result
        signer.Sign(outputPath, pkcs7);

        Console.WriteLine($"Signed PDF saved to '{outputPath}' with French caption.");
    }
}
