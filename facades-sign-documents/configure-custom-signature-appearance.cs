using System;
using System.IO;
using System.Drawing; // for Rectangle used by PdfFileSignature.Sign
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath      = "input.pdf";
        const string outputPdfPath     = "signed_output.pdf";
        const string certificatePath   = "certificate.pfx";
        const string certificatePwd    = "certPassword";
        const string foregroundImgPath = "signature_image.png";

        // Validate required files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate not found: {certificatePath}");
            return;
        }
        if (!File.Exists(foregroundImgPath))
        {
            Console.Error.WriteLine($"Foreground image not found: {foregroundImgPath}");
            return;
        }

        // Bind the PDF to the signature facade
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(inputPdfPath);

        // ------------------------------------------------------------
        // Build the custom appearance (background colour + foreground image)
        // ------------------------------------------------------------
        // The image itself is supplied via the PdfFileSignature.SignatureAppearance property.
        // The custom appearance only controls colours, labels, etc.
        Aspose.Pdf.Forms.SignatureCustomAppearance customAppearance = new Aspose.Pdf.Forms.SignatureCustomAppearance
        {
            BackgroundColor   = Aspose.Pdf.Color.LightGray,
            IsForegroundImage = true // draw the image in front of the background colour
        };

        // ------------------------------------------------------------
        // Prepare PKCS7 credentials and attach the custom appearance
        // ------------------------------------------------------------
        PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePwd);
        pkcs7.CustomAppearance = customAppearance;
        pkcs7.Reason   = "Document approval"; // optional, mirrors SignatureAppearance.Reason
        pkcs7.Location = "New York, USA";    // optional, mirrors SignatureAppearance.Location
        pkcs7.ContactInfo = "john.doe@example.com"; // optional

        // Assign the foreground image that will be shown in the visible signature
        pdfSigner.SignatureAppearance = foregroundImgPath;

        // ------------------------------------------------------------
        // Define where the visible signature will be placed
        // ------------------------------------------------------------
        // Example: place on page 1, rectangle (100,100) – (300,150)
        // PdfFileSignature.Sign expects a System.Drawing.Rectangle (x, y, width, height)
        int pageNumber = 1;
        System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 50);

        // ------------------------------------------------------------
        // Sign the document using the PKCS7 object that carries the custom appearance
        // ------------------------------------------------------------
        pdfSigner.Sign(pageNumber,
                       pkcs7.Reason,
                       pkcs7.ContactInfo,
                       pkcs7.Location,
                       true,               // make the signature visible
                       signatureRect,
                       pkcs7);

        // Save the signed PDF
        pdfSigner.Save(outputPdfPath);

        Console.WriteLine($"PDF signed successfully. Output saved to '{outputPdfPath}'.");
    }
}
