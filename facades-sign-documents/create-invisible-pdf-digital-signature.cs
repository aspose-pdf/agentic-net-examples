using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade API for signing
using Aspose.Pdf.Forms;           // PKCS1 / PKCS7 signature classes

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_hidden.pdf";  // signed output
        const string certPath   = "certificate.pfx";    // signing certificate
        const string certPass   = "password";           // certificate password

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileSignature facade and bind the PDF
        using (PdfFileSignature signer = new PdfFileSignature())
        {
            signer.BindPdf(inputPdf);

            // Associate the certificate with the signer
            signer.SetCertificate(certPath, certPass);

            // Create a PKCS1 signature object and suppress any visual properties
            PKCS1 signature = new PKCS1(certPath, certPass);
            signature.ShowProperties = false;   // hide default text (reason, date, etc.)

            // No image appearance is set (SignatureAppearance remains null)

            // Define a rectangle – required by the API but not used because visibility is false
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, 0, 0);

            // Sign on page 1 with invisible appearance (visible = false)
            signer.Sign(page: 1, visible: false, annotRect: rect, sig: signature);

            // Persist the signed PDF
            signer.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}' with hidden appearance.");
    }
}