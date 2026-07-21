using System;
using System.IO;
using System.Drawing;               // needed for Rectangle
using Aspose.Pdf.Facades;          // PdfFileSignature
using Aspose.Pdf.Forms;            // PKCS1 signature class

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed.pdf";
        const string certFile   = "certificate.pfx";
        const string certPass   = "password";

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

        // Create a PKCS#1 signature object and set its metadata
        PKCS1 signature = new PKCS1(certFile, certPass)
        {
            Reason      = "Document approved",
            ContactInfo = "john.doe@example.com",
            Location    = "New York"
        };

        // A zero‑size rectangle is required by the API but will not be rendered
        Rectangle invisibleRect = new Rectangle(0, 0, 0, 0);

        // Use the facade to sign the PDF without any visible appearance
        using (PdfFileSignature pdfSigner = new PdfFileSignature())
        {
            pdfSigner.BindPdf(inputPdf);          // load the source PDF
            // Do NOT set SignatureAppearance – leaving it unset hides the graphic
            pdfSigner.Sign(1, false, invisibleRect, signature); // page 1, invisible
            pdfSigner.Save(outputPdf);            // write the signed PDF
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}