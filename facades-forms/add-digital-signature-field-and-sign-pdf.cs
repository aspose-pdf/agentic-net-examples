using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certificate = "certificate.pfx";
        const string certPassword = "password";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificate))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificate}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdf);

        // Define the rectangle for the signature field (left, bottom, width, height)
        // Aspose.Pdf.Rectangle expects lower‑left (llx,lly) and upper‑right (urx,ury) coordinates.
        float left = 100f;
        float bottom = 100f;
        float width = 200f;
        float height = 50f;
        Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(left, bottom, left + width, bottom + height);

        // Create a signature field on page 1
        SignatureField sigField = new SignatureField(doc.Pages[1], fieldRect)
        {
            PartialName = "DigitalSignature"
        };
        // Add the field to the document's form (page index is 1‑based)
        doc.Form.Add(sigField, 1);

        // Prepare the PDF signer
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(doc);

        // Load the certificate (PKCS#1 format)
        PKCS1 pkcs = new PKCS1(certificate, certPassword);

        // Appearance rectangle for the visible signature (System.Drawing.Rectangle)
        System.Drawing.Rectangle appearanceRect = new System.Drawing.Rectangle((int)left, (int)bottom, (int)width, (int)height);

        // Signing options
        string location = "Office";
        string reason = "Document approval";
        string contactInfo = "email@example.com";

        // Sign the PDF on page 1, make the signature visible, and use the created field
        pdfSigner.Sign(1, reason, contactInfo, location, true, appearanceRect, pkcs);

        // Save the signed PDF
        pdfSigner.Save(outputPdf);

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
