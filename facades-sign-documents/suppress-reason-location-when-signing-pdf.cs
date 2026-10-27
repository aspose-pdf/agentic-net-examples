using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // required for PKCS7 and SignatureCustomAppearance

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

        // Bind the PDF to the signature facade
        var signature = new PdfFileSignature();
        signature.BindPdf(inputPath);

        // Create a PKCS7 signature object and suppress location/reason/contact text
        var pkcs = new PKCS7(certificatePath, certificatePassword)
        {
            Location = string.Empty,   // suppress location
            Reason = string.Empty,     // suppress reason
            ContactInfo = string.Empty // suppress contact info
        };

        // Optional: hide the labels on the appearance as well
        pkcs.CustomAppearance = new SignatureCustomAppearance
        {
            ShowLocation = false,
            ShowReason = false,
            ShowContactInfo = false
        };

        // Sign the PDF using the PKCS7 object. The overload without SignatureFieldInfo
        // signs the whole document with a default signature field.
        signature.Sign(outputPath, pkcs);

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
