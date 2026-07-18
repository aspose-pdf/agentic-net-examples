using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to input PDF, output PDF, certificate and appearance image
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed.pdf";
        const string certFile = "certificate.pfx";
        const string certPassword = "password";
        const string appearanceImg = "signature.png";

        // Verify required files exist
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
        if (!File.Exists(appearanceImg))
        {
            Console.Error.WriteLine($"Signature appearance image not found: {appearanceImg}");
            return;
        }

        // Initialize the facade and bind the PDF document
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(inputPdf);

        // Set the image that will be used as the signature appearance
        pdfSigner.SignatureAppearance = appearanceImg;

        // Create a PKCS#1 signature object and set basic properties
        PKCS1 pkcs1Signature = new PKCS1(certFile, certPassword)
        {
            Reason = "Document approved",
            ContactInfo = "john.doe@example.com",
            Location = "New York"
        };

        // Configure custom appearance: background color and draw the image in the foreground
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance
        {
            BackgroundColor = Aspose.Pdf.Color.LightGray,
            IsForegroundImage = true,               // image will be drawn over the background
            ForegroundColor = Aspose.Pdf.Color.Blue // optional text color
        };
        pkcs1Signature.CustomAppearance = customAppearance;

        // Define the rectangle where the signature will be placed (System.Drawing.Rectangle)
        System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Sign the first page, make the signature visible
        pdfSigner.Sign(page: 1, visible: true, annotRect: signatureRect, sig: pkcs1Signature);

        // Save the signed PDF
        pdfSigner.Save(outputPdf);
        pdfSigner.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}