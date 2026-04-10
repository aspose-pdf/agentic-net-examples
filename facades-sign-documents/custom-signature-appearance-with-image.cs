using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf        = "input.pdf";
        const string outputPdf       = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePwd  = "password";
        const string appearanceImg   = "signature.png";

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(certificatePath) || !File.Exists(appearanceImg))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Initialize the signature facade and bind the source PDF
        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPdf);

        // Set the image that will be used for the signature appearance
        pdfSign.SignatureAppearance = appearanceImg;

        // Define the rectangle (System.Drawing.Rectangle) where the signature will be placed
        System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 100); // x, y, width, height

        // Create a PKCS#7 signature object with the certificate
        PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePwd);
        pkcs7.Reason      = "Document approved";
        pkcs7.ContactInfo = "john.doe@example.com";
        pkcs7.Location    = "New York";

        // Configure custom appearance: background color and draw the image as foreground
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
        customAppearance.BackgroundColor   = Aspose.Pdf.Color.LightGray; // background color
        customAppearance.IsForegroundImage = true;                       // image drawn in foreground
        pkcs7.CustomAppearance = customAppearance;

        // Sign the document on page 1 using the prepared signature object
        pdfSign.Sign(page: 1, visible: true, annotRect: signatureRect, sig: pkcs7);

        // Save the signed PDF
        pdfSign.Save(outputPdf);
        pdfSign.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
