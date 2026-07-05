using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfFileSignature, PKCS1
using Aspose.Pdf.Forms;           // Signature base class

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and certificate details
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed.pdf";
        const string certPath  = "certificate.pfx";
        const string certPwd   = "password";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the facade for signing
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the PDF to be signed
            pdfSign.BindPdf(inputPdf);

            // Load the signing certificate
            pdfSign.SetCertificate(certPath, certPwd);

            // Optional: set a graphic appearance for the signature
            // pdfSign.SignatureAppearance = "signatureImage.png";

            // Create a PKCS#1 signature object
            PKCS1 signature = new PKCS1(certPath, certPwd);

            // Suppress reason and location by assigning empty strings
            signature.Reason      = string.Empty;   // Reason text will be empty
            signature.Location    = string.Empty;   // Location text will be empty
            signature.ContactInfo = string.Empty;   // Optional: also clear contact info

            // Define the rectangle where the visible signature will appear
            // (x, y, width, height) – using System.Drawing.Rectangle explicitly
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Sign page 1, make the signature visible, using the prepared signature object
            pdfSign.Sign(page: 1, visible: true, annotRect: rect, sig: signature);

            // Save the signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}