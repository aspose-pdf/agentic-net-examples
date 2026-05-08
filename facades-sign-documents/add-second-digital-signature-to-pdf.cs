using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";          // source PDF (already contains a signature)
        const string outputPdf     = "signed_output.pdf";  // result PDF with the second signature
        const string certPath      = "certificate.pfx";    // signing certificate
        const string certPassword  = "password";           // certificate password
        const string appearanceImg = "signature.png";      // optional visual appearance for the signature

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

        // Use PdfFileSignature facade to add a second digital signature on page 3
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the existing PDF document
            pdfSign.BindPdf(inputPdf);

            // Configure signing certificate and optional appearance image
            pdfSign.SetCertificate(certPath, certPassword);
            pdfSign.SignatureAppearance = appearanceImg; // can be omitted if no visual stamp is needed

            // Define the rectangle area for the new signature (x, y, width, height) in points
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 50);

            // Add the second signature on page 3
            pdfSign.Sign(
                page: 3,                     // target page (1‑based indexing)
                SigReason: "Second signature",
                SigContact: "contact@example.com",
                SigLocation: "New York",
                visible: true,               // make the signature visible
                annotRect: rect);            // rectangle where the signature will appear

            // Save the signed PDF to a new file
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Second digital signature added and saved to '{outputPdf}'.");
    }
}