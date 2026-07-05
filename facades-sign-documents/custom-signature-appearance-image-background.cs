using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input and output files
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed.pdf";

        // Certificate for signing
        const string certFile = "certificate.pfx";
        const string certPassword = "password";

        // Image to be used as signature appearance
        const string appearanceImage = "signature.png";

        // Verify that required files exist
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
        if (!File.Exists(appearanceImage))
        {
            Console.Error.WriteLine($"Signature appearance image not found: {appearanceImage}");
            return;
        }

        // Create the PdfFileSignature facade and configure it
        using (Aspose.Pdf.Facades.PdfFileSignature pdfSigner = new Aspose.Pdf.Facades.PdfFileSignature())
        {
            // Bind the source PDF
            pdfSigner.BindPdf(inputPdf);

            // Set the certificate for signing
            pdfSigner.SetCertificate(certFile, certPassword);

            // Set the image that will be used as the visual appearance of the signature
            pdfSigner.SignatureAppearance = appearanceImage;

            // Create a custom appearance object
            Aspose.Pdf.Forms.SignatureCustomAppearance customAppearance = new Aspose.Pdf.Forms.SignatureCustomAppearance
            {
                // Set a background color for the signature field
                BackgroundColor = Aspose.Pdf.Color.LightGray,

                // Draw the image as a foreground element (over the background)
                IsForegroundImage = true
            };

            // Create a PKCS#1 signature object and assign custom appearance
            Aspose.Pdf.Forms.PKCS1 pkcs1Signature = new Aspose.Pdf.Forms.PKCS1(certFile, certPassword)
            {
                Reason = "Document approved",
                ContactInfo = "contact@example.com",
                Location = "New York",
                CustomAppearance = customAppearance
            };

            // Define the rectangle (System.Drawing.Rectangle) where the signature will be placed
            System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Sign the first page (page numbers are 1‑based)
            pdfSigner.Sign(1, true, signatureRect, pkcs1Signature);

            // Save the signed PDF
            pdfSigner.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}