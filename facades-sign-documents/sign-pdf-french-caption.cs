using System;
using System.Drawing;
using System.Globalization;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_french.pdf";
        const string pfxPath   = "certificate.pfx";
        const string pfxPass   = "password";

        // Verify input files exist
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!System.IO.File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Initialize the facade for signing
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(inputPdf);

        // Create PKCS#7 signature object
        PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPass);

        // Customize appearance – set French culture for caption labels
        SignatureCustomAppearance appearance = new SignatureCustomAppearance
        {
            Culture = new CultureInfo("fr-FR") // French (France)
            // Optional: override specific labels if desired
            // DigitalSignedLabel = "Signé numériquement par",
            // ReasonLabel = "Raison",
            // LocationLabel = "Lieu",
            // DateSignedAtLabel = "Date"
        };
        pkcs7.CustomAppearance = appearance;

        // Optional metadata (can be left empty or set to French equivalents)
        pkcs7.Reason = "Approbation du document";
        pkcs7.Location = "Paris, France";
        pkcs7.ContactInfo = "contact@example.com";

        // Define visible signature rectangle (x, y, width, height)
        Rectangle signatureRect = new Rectangle(100, 100, 200, 100);

        // Sign page 1, make the signature visible
        pdfSigner.Sign(page: 1, visible: true, annotRect: signatureRect, sig: pkcs7);

        // Save the signed PDF
        pdfSigner.Save(outputPdf);

        // Release resources
        pdfSigner.Close();

        Console.WriteLine($"PDF signed and saved to '{outputPdf}'.");
    }
}