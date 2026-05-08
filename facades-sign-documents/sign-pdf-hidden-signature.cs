using System;
using System.IO;
using System.Drawing;               // for Rectangle
using Aspose.Pdf.Facades;   // PdfFileSignature facade
using Aspose.Pdf.Forms;    // PKCS1 signature class

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_hidden.pdf";
        const string certFile  = "certificate.pfx";
        const string certPass  = "password";

        // Validate required files
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

        // Create the signing facade and bind the source PDF
        using (PdfFileSignature signer = new PdfFileSignature())
        {
            signer.BindPdf(inputPdf);

            // Build a PKCS1 signature object (cryptographic data only)
            PKCS1 pkcs1 = new PKCS1(certFile, certPass)
            {
                Reason      = "Approved for release",
                ContactInfo = "john.doe@example.com",
                Location    = "New York"
            };

            // An empty rectangle – the signature will be invisible
            Rectangle dummyRect = Rectangle.Empty;

            // Sign page 1 with invisible appearance (visible = false)
            // Use positional arguments because the overload does not support named parameters.
            signer.Sign(
                1,                     // page number (1‑based)
                pkcs1.Reason,
                pkcs1.ContactInfo,
                pkcs1.Location,
                false,                 // visible flag – false hides the appearance
                dummyRect,
                pkcs1);

            // Persist the signed PDF
            signer.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}' (signature appearance hidden).");
    }
}
