using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfFileSignature
using Aspose.Pdf.Forms;           // PKCS1 signature class
using System.Drawing;             // Rectangle (required by Sign method)

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and certificate details
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath  = "certificate.pfx";
        const string certPwd   = "password";

        // Ensure the input files exist
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

        // Initialize the facade and bind the PDF file
        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPdf);

        // Set the certificate for the signing routine (optional when using PKCS1 ctor)
        pdfSign.SetCertificate(certPath, certPwd);

        // Define the signature appearance rectangle (position and size)
        Rectangle signRect = new Rectangle(100, 100, 200, 100); // x, y, width, height

        // Create a PKCS1 signature object and clear location/reason/contact strings
        PKCS1 signature = new PKCS1(certPath, certPwd);
        signature.Reason      = string.Empty; // suppress reason text
        signature.Location    = string.Empty; // suppress location text
        signature.ContactInfo = string.Empty; // optional: suppress contact text

        // Sign the document:
        // page = 1 (first page), visible = true, rectangle = signRect, signature = PKCS1 object
        pdfSign.Sign(page: 1, visible: true, annotRect: signRect, sig: signature);

        // Save the signed PDF
        pdfSign.Save(outputPdf);

        // Release resources held by the facade
        pdfSign.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}