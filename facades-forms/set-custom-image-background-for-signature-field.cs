using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfFileSignature
using Aspose.Pdf.Forms;           // PKCS1, SignatureCustomAppearance

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";          // PDF containing a signature field named "Signature"
        const string outputPdf  = "output.pdf";
        const string imagePath  = "signature_bg.jpg";   // Image to be used as background
        const string certPath   = "certificate.pfx";    // Valid certificate file
        const string certPass   = "password";           // Certificate password

        // Validate required files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Signature background image not found: {imagePath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Use PdfFileSignature facade to modify the signature field appearance
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the existing PDF document
            pdfSign.BindPdf(inputPdf);

            // Set the image that will be used as the signature appearance background
            pdfSign.SignatureAppearance = imagePath;   // property expects a file name

            // Create a signature object (PKCS1) – this holds the certificate and appearance settings
            PKCS1 signature = new PKCS1(certPath, certPass);

            // Configure a custom appearance object
            SignatureCustomAppearance customAppearance = new SignatureCustomAppearance
            {
                // Image will be drawn as background (default), so keep IsForegroundImage = false
                IsForegroundImage = false,
                // Optional visual tweaks
                BackgroundColor = Aspose.Pdf.Color.LightGray,
                FontFamilyName = "Arial",
                FontSize = 10,
                ForegroundColor = Aspose.Pdf.Color.Blue
            };

            // Assign the custom appearance to the signature
            signature.CustomAppearance = customAppearance;

            // Apply the signature (and its appearance) to the existing field named "Signature"
            // The field must already exist in the PDF.
            pdfSign.Sign("Signature", signature);

            // Save the updated PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signature field appearance updated and saved to '{outputPdf}'.");
    }
}