using System;
using System.IO;
using System.Globalization;
using Aspose.Pdf.Facades;          // PdfFileSignature
using Aspose.Pdf.Forms;           // PKCS1, SignatureCustomAppearance

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "signed_french.pdf";
        const string certPath   = "certificate.pfx";
        const string certPassword = "password";

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

        // Initialize the facade and bind the source PDF
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);

            // Define the signature rectangle (System.Drawing.Rectangle is required by the API)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Create a PKCS#1 signature object using the certificate
            PKCS1 signature = new PKCS1(certPath, certPassword);
            signature.Reason      = "Approval";
            signature.ContactInfo = "contact@example.com";
            signature.Location    = "Paris";

            // Configure custom appearance to use French locale
            SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
            customAppearance.Culture = new CultureInfo("fr-FR");               // French culture
            customAppearance.DigitalSignedLabel = "Signé numériquement par"; // Optional label translation
            signature.CustomAppearance = customAppearance;

            // Apply the signature on page 1 (visible) with the defined rectangle
            pdfSign.Sign(1, true, rect, signature);

            // Save the signed PDF
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}