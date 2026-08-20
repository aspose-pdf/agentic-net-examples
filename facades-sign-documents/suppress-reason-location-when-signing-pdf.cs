using System;
using System.IO;
using System.Drawing;                     // for Rectangle
using Aspose.Pdf.Facades;                // PdfFileSignature
using Aspose.Pdf.Forms;                  // PKCS1 signature class

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed.pdf";
        const string certFile   = "certificate.pfx";
        const string certPass   = "password";

        // Verify input files exist
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

        // Create the facade, bind the PDF and configure the certificate
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPdf);
            pdfSign.SetCertificate(certFile, certPass);

            // Create a PKCS1 signature object and clear Reason/Location (and ContactInfo)
            PKCS1 signature = new PKCS1(certFile, certPass);
            signature.Reason      = "";   // suppress reason text
            signature.Location    = "";   // suppress location text
            signature.ContactInfo = "";   // optional: clear contact info

            // Define the visible signature rectangle (x, y, width, height)
            Rectangle rect = new Rectangle(100, 100, 200, 50);

            // Sign page 1 (visible) using the prepared signature object
            pdfSign.Sign(page: 1, visible: true, annotRect: rect, sig: signature);

            // Save the signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}