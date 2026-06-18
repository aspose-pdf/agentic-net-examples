using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";
        const string appearanceImage = "signature_bg.png";

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(certPath) || !File.Exists(appearanceImage))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Initialize the PdfFileSignature facade
        using (PdfFileSignature signer = new PdfFileSignature())
        {
            // Bind the source PDF document
            signer.BindPdf(inputPdf);

            // Set the image that will be used as the signature appearance
            signer.SignatureAppearance = appearanceImage;

            // Create a PKCS#1 signature object with the certificate
            PKCS1 pkcs1 = new PKCS1(certPath, certPassword);
            pkcs1.Reason = "I agree";
            pkcs1.ContactInfo = "contact@example.com";
            pkcs1.Location = "New York, USA";

            // Create a custom appearance and configure it to use the image as a background
            SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
            customAppearance.IsForegroundImage = false; // image drawn as background
            customAppearance.FontFamilyName = "Arial";
            customAppearance.FontSize = 12;
            customAppearance.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Assign the custom appearance to the signature object
            pkcs1.CustomAppearance = customAppearance;

            // Define the signature rectangle (x, y, width, height)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Sign the document on page 1, making the signature visible
            signer.Sign(1, true, rect, pkcs1);

            // Save the signed PDF
            signer.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}