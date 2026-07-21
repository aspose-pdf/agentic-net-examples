using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and certificate details
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certFile = "certificate.pfx";
        const string certPassword = "password";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certFile))
        {
            Console.Error.WriteLine($"Certificate file not found: {certFile}");
            return;
        }

        // Create the facade for signing
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(inputPdf);

        // Optional: set a graphic appearance (image) for the signature field
        // pdfSigner.SignatureAppearance = "signature_image.png";

        // Define the signature rectangle (position and size) on the page
        // System.Drawing.Rectangle is used as required by the API
        System.Drawing.Rectangle sigRect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Create a PKCS#1 signature object and set its properties
        PKCS1 pkcs1Signature = new PKCS1(certFile, certPassword)
        {
            Reason = "Approved",
            ContactInfo = "john.doe@example.com",
            Location = "New York"
        };

        // Hide the default "Digitally signed by" caption
        // Option 1: disable all default property strings
        pkcs1Signature.ShowProperties = false;

        // Option 2 (alternative): provide a custom appearance with an empty label
        // SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
        // customAppearance.DigitalSignedLabel = string.Empty; // hide label
        // pkcs1Signature.CustomAppearance = customAppearance;

        // Sign the document on page 1, make the signature visible, using the defined rectangle
        pdfSigner.Sign(page: 1, visible: true, annotRect: sigRect, sig: pkcs1Signature);

        // Save the signed PDF
        pdfSigner.Save(outputPdf);

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}