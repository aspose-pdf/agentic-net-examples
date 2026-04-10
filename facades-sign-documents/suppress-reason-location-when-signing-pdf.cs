using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade API for signing
using Aspose.Pdf.Forms;           // PKCS1, PKCS7 signature classes

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

        // Validate required files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Use PdfFileSignature facade to open, sign and save the document
        using (PdfFileSignature signer = new PdfFileSignature())
        {
            // Bind the existing PDF file
            signer.BindPdf(inputPdf);

            // Set the certificate that will be used for signing
            signer.SetCertificate(certPath, certPass);

            // Create a PKCS1 signature object (any Signature-derived type works)
            PKCS1 signature = new PKCS1(certPath, certPass);

            // Suppress Reason and Location by assigning empty strings
            signature.Reason   = string.Empty;
            signature.Location = string.Empty;

            // Optionally hide all default property strings (Reason, Location, Contact, etc.)
            signature.ShowProperties = false;

            // Define the visible signature rectangle (System.Drawing.Rectangle)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Sign page 1, make the signature visible, using the prepared signature object
            signer.Sign(page: 1, visible: true, annotRect: rect, sig: signature);

            // Save the signed PDF to the output path
            signer.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}