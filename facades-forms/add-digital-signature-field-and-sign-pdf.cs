using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // PdfFileSignature and PKCS7 are defined here
using Aspose.Pdf.Forms;   // SignatureField and PKCS7 live in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath = "certificate.pfx";   // PKCS#12 certificate file
        const string certPass = "certPassword";     // certificate password

        // Verify required files exist
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

        // ------------------------------------------------------------
        // 1. Add a signature field named "DigitalSignature" to the PDF.
        // ------------------------------------------------------------
        Document doc = new Document(inputPdf);
        // Define the rectangle for the signature field (lower‑left X,Y, upper‑right X,Y)
        Rectangle sigRect = new Rectangle(100, 100, 250, 150);
        // Create the signature field on page 1
        SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
        {
            PartialName = "DigitalSignature"
        };
        // Add the field to the form (page index is 1‑based)
        doc.Form.Add(sigField, 1);
        // Save the PDF with the new field to a temporary location (or overwrite the original)
        string tempPdf = Path.GetTempFileName();
        doc.Save(tempPdf);

        // ------------------------------------------------------------
        // 2. Sign the PDF using the certificate and set default appearance.
        // ------------------------------------------------------------
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(tempPdf);

        // Create PKCS7 object with the certificate and set appearance information
        PKCS7 pkcs7 = new PKCS7(certPath, certPass)
        {
            ContactInfo = "contact@example.com",
            Location    = "Office",
            Reason      = "Document approval"
        };

        // Sign the PDF. The overload that accepts PKCS7 writes the signed document to the output path.
        pdfSignature.Sign(outputPdf, pkcs7);

        // Clean up temporary file
        File.Delete(tempPdf);

        Console.WriteLine($"Signature field added and PDF signed: {outputPdf}");
    }
}
