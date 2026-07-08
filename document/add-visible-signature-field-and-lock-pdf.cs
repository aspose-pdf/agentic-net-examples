using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_locked.pdf"; // result PDF
        const string pfxPath    = "certificate.pfx";   // signing certificate
        const string pfxPassword = "password";         // certificate password

        // Ensure the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF (using the mandatory lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Create a visible signature field on page 1
            // -----------------------------------------------------------------
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                Name        = "Signature1",
                PartialName = "Signature1",
                // Optional tooltip shown in PDF viewers
                AlternateName = "Document Signature"
            };

            // Add the field to page 1 of the form
            doc.Form.Add(sigField, 1);

            // -----------------------------------------------------------------
            // 2. Prepare the digital signature object (visible appearance is
            //    provided automatically by the signature field)
            // -----------------------------------------------------------------
            // Use the concrete PKCS7 class – Signature is abstract.
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason      = "Approved",
                Location    = "Head Office",
                ContactInfo = "contact@example.com"
            };

            // -----------------------------------------------------------------
            // 3. Sign the document using the created field
            // -----------------------------------------------------------------
            sigField.Sign(pkcs7);

            // -----------------------------------------------------------------
            // 4. Lock the document after signing (no further modifications)
            // -----------------------------------------------------------------
            // Prevent further changes to existing signatures – only append is allowed.
            doc.Form.SignaturesAppendOnly = true;

            // -----------------------------------------------------------------
            // 5. Save the signed and locked PDF (using the mandatory lifecycle rule)
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and locked PDF saved to '{outputPdf}'.");
    }
}
