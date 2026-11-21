using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // SignatureField, PKCS7, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath    = "input.pdf";
        const string outputPdfPath   = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePwd  = "certPassword";
        const string tsaUrl          = "http://timestamp.digicert.com"; // trusted TSA URL

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page (pages are 1‑based)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(pdfDoc.Pages[1], sigRect)
            {
                PartialName = "Signature1"
            };
            // Add the signature field to the document's form collection
            pdfDoc.Form.Add(sigField);

            // Prepare the PKCS7 signature (digital signature) with timestamping
            PKCS7 pkcs = new PKCS7(certificatePath, certificatePwd)
            {
                Reason      = "Document approval",
                Location    = "Company Headquarters",
                ContactInfo = "contact@example.com",
                // Configure timestamping from a trusted TSA
                TimestampSettings = new TimestampSettings(tsaUrl, null)
            };

            // Sign the field (Signature property is read‑only; use Sign method)
            sigField.Sign(pkcs);

            // Save the signed PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
