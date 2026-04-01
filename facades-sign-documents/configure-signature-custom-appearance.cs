using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";
        const string appearanceImage = "signature.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine("Certificate file not found: " + certificatePath);
            return;
        }
        if (!File.Exists(appearanceImage))
        {
            Console.Error.WriteLine("Signature appearance image not found: " + appearanceImage);
            return;
        }

        try
        {
            // Bind the PDF to the signature facade
            PdfFileSignature pdfSignature = new PdfFileSignature();
            pdfSignature.BindPdf(inputPdf);

            // Set the image that will be used for the signature appearance
            pdfSignature.SignatureAppearance = appearanceImage;

            // Create PKCS7 signature object
            PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword);

            // Configure custom appearance: background color and foreground image
            SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
            customAppearance.BackgroundColor = Aspose.Pdf.Color.LightGray;
            customAppearance.IsForegroundImage = true; // draw the image in front of the text
            // Additional customizations (optional) can be set here, e.g., fonts, labels, etc.
            pkcs7.CustomAppearance = customAppearance;

            // Define the rectangle where the signature will be placed (x, y, width, height)
            System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Sign page 1 with a visible signature
            pdfSignature.Sign(1, "Document approved", "contact@example.com", "New York", true, signatureRect, pkcs7);

            // Save the signed PDF
            pdfSignature.Save(outputPdf);
            pdfSignature.Close();

            Console.WriteLine("Signed PDF saved to '" + outputPdf + "'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}