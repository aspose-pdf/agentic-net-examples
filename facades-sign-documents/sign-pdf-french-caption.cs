using System;
using System.IO;
using System.Globalization;
using System.Drawing;
using Aspose.Pdf.Facades;          // PdfFileSignature facade
using Aspose.Pdf.Forms;           // PKCS7 signature and SignatureCustomAppearance

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_french.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(certPath))
        {
            Console.Error.WriteLine("Input PDF or certificate file not found.");
            return;
        }

        // Initialize the signature facade and bind the source PDF
        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPdf);

        // OPTIONAL: set a graphic image for the signature appearance
        // pdfSign.SignatureAppearance = "signature_image.png";

        // Create a PKCS7 signature object with certificate (PKCS7 is the correct class for digital signatures)
        PKCS7 signature = new PKCS7(certPath, certPass);
        signature.Reason      = "Document signé";          // Reason text
        signature.ContactInfo = "contact@example.com";    // Contact information (use ContactInfo, not Contact)
        signature.Location    = "Paris";                  // Location text

        // Configure custom appearance to use French locale
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
        customAppearance.Culture = new CultureInfo("fr-FR"); // French (France)
        signature.CustomAppearance = customAppearance;       // Apply to signature

        // Define the visible rectangle for the signature (System.Drawing.Rectangle)
        Rectangle rect = new Rectangle(100, 100, 200, 100);

        // Sign page 1 (1‑based indexing), make it visible, using the custom signature
        pdfSign.Sign(page: 1, visible: true, annotRect: rect, sig: signature);

        // Save the signed PDF to the output file
        pdfSign.Save(outputPdf);
        pdfSign.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
