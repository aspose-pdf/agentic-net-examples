using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_final.pdf";
        const string pfxPath    = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Create a signature field on the first page.
            // -----------------------------------------------------------------
            // Rectangle is defined as (llx, lly, urx, ury) in points.
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                // The name of the signature field – must be unique within the document.
                PartialName = "Signature1"
            };

            // Add the signature field to the document's form.
            // The second argument (1) specifies the field order.
            doc.Form.Add(sigField, 1);

            // -----------------------------------------------------------------
            // 2. Prepare the digital signature object.
            // -----------------------------------------------------------------
            // PKCS1 is a simple RSA‑based signature. It can be created from a
            // PFX file (certificate + private key) and a password.
            Aspose.Pdf.Forms.PKCS1 pkcs1Signature = new Aspose.Pdf.Forms.PKCS1(pfxPath, pfxPassword)
            {
                Reason      = "Document approved",
                ContactInfo = "signer@example.com",
                Location    = "Head Office"
            };

            // -----------------------------------------------------------------
            // 3. Sign the field.
            // -----------------------------------------------------------------
            // The Sign method attaches the digital signature to the field.
            sigField.Sign(pkcs1Signature);

            // -----------------------------------------------------------------
            // 4. Enforce finality – no further signatures allowed.
            // -----------------------------------------------------------------
            // Setting SignaturesAppendOnly to true makes any subsequent
            // modifications that would alter existing signatures invalid.
            // Combined with a certification signature (access permission
            // NoChanges) this effectively locks the document.
            doc.Form.SignaturesAppendOnly = true;

            // -----------------------------------------------------------------
            // 5. Save the signed PDF.
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed and locked: {outputPdf}");
    }
}