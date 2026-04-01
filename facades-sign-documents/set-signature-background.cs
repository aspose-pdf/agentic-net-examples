using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Create a custom appearance with a semi‑transparent background (50% opacity white)
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
        customAppearance.BackgroundColor = Aspose.Pdf.Color.FromArgb(128, 255, 255, 255);

        // Create PKCS7 signature and assign the custom appearance
        PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword);
        pkcs7.CustomAppearance = customAppearance;

        // Bind the PDF and apply the signature
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPath);
        // Optional: set an image for the signature appearance
        // pdfSignature.SignatureAppearance = "signature.png";

        System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 50);
        pdfSignature.Sign(1, true, signatureRect, pkcs7);
        pdfSignature.Save(outputPath);
        pdfSignature.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}