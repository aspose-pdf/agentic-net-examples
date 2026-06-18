using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input PDF, output signed PDF, and certificate (PFX) details
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_final.pdf";
        const string certPath  = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure that any further signatures must be added as incremental updates only.
            // This prevents accidental invalidation of the existing signature.
            doc.Form.SignaturesAppendOnly = true;

            // Define the rectangle where the visible signature will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field and add it to the document's form
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField);

            // Create a PKCS#7 signature object using the certificate
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason      = "Document approved",
                ContactInfo = "signer@example.com",
                Location    = "New York"
            };

            // Sign the field with the PKCS#7 signature
            sigField.Sign(pkcs7);

            // Save the signed PDF. The signature is embedded and the document is now
            // marked as append‑only, disallowing further modifications that would
            // invalidate the signature.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed and finalized: {outputPdf}");
    }
}