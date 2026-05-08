using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;          // PKCS1, SignatureCustomAppearance
using AspNetCore = Aspose.Pdf;   // alias to avoid ambiguity if needed

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, certificate and appearance image paths
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string certPath   = "certificate.pfx";
        const string certPwd    = "password";
        const string imagePath  = "signature_image.png";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate not found: {certPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Signature image not found: {imagePath}");
            return;
        }

        // Create the facade, bind the PDF and configure appearance
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the source PDF
            pdfSign.BindPdf(inputPdf);

            // Set the foreground image that will be drawn inside the signature rectangle
            pdfSign.SignatureAppearance = imagePath;   // image file name

            // Create a PKCS1 signature object and assign certificate
            PKCS1 pkcs1Signature = new PKCS1(certPath, certPwd);

            // Configure custom appearance: background color and draw image as foreground
            SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
            customAppearance.BackgroundColor = Aspose.Pdf.Color.LightGray; // background behind the image
            customAppearance.IsForegroundImage = true;                     // draw the image on top
            // (optional) set text color, font, etc. if needed
            // customAppearance.ForegroundColor = Aspose.Pdf.Color.Blue;

            pkcs1Signature.CustomAppearance = customAppearance;

            // Define the rectangle where the signature will be placed (System.Drawing.Rectangle)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Sign the document (page numbers are 1‑based)
            pdfSign.Sign(
                page: 1,
                SigReason: "Approved",
                SigContact: "john.doe@example.com",
                SigLocation: "New York",
                visible: true,
                annotRect: rect,
                sig: pkcs1Signature);

            // Save the signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}