using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Create a PKCS7 signature object (cryptographic part only)
        PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword);
        pkcs7Signature.Reason = "Signing without visible appearance";
        pkcs7Signature.ContactInfo = "example@example.com";
        pkcs7Signature.Location = "Location";

        // Use PdfFileSignature to apply the digital signature
        using (PdfFileSignature pdfSigner = new PdfFileSignature())
        {
            pdfSigner.BindPdf(inputPath);
            pdfSigner.SetCertificate(certificatePath, certificatePassword);

            // Rectangle is required by the API but will not be shown because visibility is false
            System.Drawing.Rectangle dummyRect = new System.Drawing.Rectangle(0, 0, 0, 0);

            // Sign page 1, invisible appearance
            pdfSigner.Sign(1, false, dummyRect, pkcs7Signature);
            pdfSigner.Save(outputPath);
        }

        Console.WriteLine($"PDF signed (invisible appearance) saved to '{outputPath}'.");
    }
}
