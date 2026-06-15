using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;      // PdfFileSignature
using Aspose.Pdf.Forms;       // PKCS1, Signature

class Program
{
    static void Main()
    {
        // Paths to the source PDF, output PDF and the signing certificate
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed.pdf";
        const string certPath  = "certificate.pfx";
        const string certPwd   = "password";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the facade and bind the PDF to be signed
        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPdf);

        // Optional: set a graphic appearance for the signature (image file)
        // pdfSign.SignatureAppearance = "signatureAppearance.png";

        // Create a PKCS#1 signature object using the certificate
        PKCS1 signature = new PKCS1(certPath, certPwd);

        // Suppress the Reason and Location text by assigning empty strings
        signature.Reason      = string.Empty;   // no reason displayed
        signature.Location    = string.Empty;   // no location displayed
        signature.ContactInfo = string.Empty;   // optional: clear contact info as well

        // Define the rectangle where the visible signature will be placed
        // (x, y, width, height) – coordinates are in points (1/72 inch)
        Rectangle rect = new Rectangle(100, 100, 200, 100);

        // Sign page 1, make the signature visible, using the prepared signature object
        pdfSign.Sign(page: 1, visible: true, annotRect: rect, sig: signature);

        // Save the signed PDF to the output file
        pdfSign.Save(outputPdf);
        pdfSign.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}