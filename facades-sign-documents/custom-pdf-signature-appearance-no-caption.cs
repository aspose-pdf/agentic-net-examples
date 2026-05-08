using System;
using System.Drawing;                     // For Rectangle
using Aspose.Pdf.Facades;                // PdfFileSignature
using Aspose.Pdf.Forms;                  // PKCS7, SignatureCustomAppearance

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
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
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Create a visible signature on page 1
        const int pageNumber = 1;
        var rect = new Rectangle(100, 100, 200, 50); // x, y, width, height

        // Prepare the PKCS#7 signature object
        PKCS7 pkcs7 = new PKCS7(certPath, certPass);
        pkcs7.Reason   = "Approved";
        pkcs7.ContactInfo = "contact@example.com";
        pkcs7.Location = "New York, USA";

        // Create a custom appearance and hide the default caption
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
        customAppearance.DigitalSignedLabel = string.Empty; // hides "Digitally signed by"
        // (optional) adjust other labels or styling as needed
        pkcs7.CustomAppearance = customAppearance;

        // Sign the PDF using PdfFileSignature
        using (PdfFileSignature signer = new PdfFileSignature())
        {
            signer.BindPdf(inputPdf);                     // Load the PDF
            signer.SetCertificate(certPath, certPass);    // Set certificate for signing
            signer.Sign(pageNumber, pkcs7.Reason, pkcs7.ContactInfo,
                        pkcs7.Location, true, rect, pkcs7);
            signer.Save(outputPdf);                       // Save the signed PDF
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}