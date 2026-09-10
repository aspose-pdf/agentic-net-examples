using System;
using System.Drawing;
using System.Globalization;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_french.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

        // Verify input files exist
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!System.IO.File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate not found: {certPath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPdf);

        // Load the signing certificate (optional when using PKCS1 constructor)
        pdfSign.SetCertificate(certPath, certPass);

        // Create a PKCS#1 signature object with French locale for the caption
        PKCS1 signature = new PKCS1(certPath, certPass)
        {
            Reason      = "Document approved",
            ContactInfo = "contact@example.com",
            Location    = "Paris",
            CustomAppearance = new SignatureCustomAppearance
            {
                Culture = new CultureInfo("fr-FR") // French (France)
            }
        };

        // Define the rectangle where the visible signature will be placed (System.Drawing.Rectangle)
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Sign the document on page 1, make the signature visible
        // Use the overload: Sign(int pageNumber, string reason, string contactInfo, string location,
        //                     bool visible, Rectangle rect, PKCS1 pkcs1)
        pdfSign.Sign(
            1,                     // page number (1‑based)
            signature.Reason,      // reason
            signature.ContactInfo, // contact info
            signature.Location,    // location
            true,                  // visible signature flag
            rect,                  // signature rectangle
            signature);            // PKCS1 signature object

        // Save the signed PDF
        pdfSign.Save(outputPdf);
        pdfSign.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPdf}' with French caption.");
    }
}
