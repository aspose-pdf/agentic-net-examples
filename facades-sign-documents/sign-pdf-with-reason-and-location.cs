using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "certPassword";

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Use PdfFileSignature inside a using block (it implements IDisposable)
        using (PdfFileSignature signer = new PdfFileSignature())
        {
            // Load the source PDF
            signer.BindPdf(inputPath);

            // Define a visible signature rectangle (x, y, width, height)
            Rectangle signatureRect = new Rectangle(100, 100, 200, 50);

            // Create PKCS7 object from the certificate (used for the digital signature)
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Approved for release", // set signature reason
                Location = "New York",           // set signature location
                ContactInfo = string.Empty        // optional – suppress contact field
            };

            // Sign page 1 with a visible signature using the PKCS7 object.
            // The overload Sign(int pageNumber, bool visible, Rectangle rect, PKCS7 signature)
            // picks up Reason, Location and ContactInfo from the PKCS7 instance.
            signer.Sign(1, true, signatureRect, pkcs7);

            // Save the signed PDF to the desired output path
            signer.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
