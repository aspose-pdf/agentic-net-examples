using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_locked.pdf"; // result PDF
        const string pfxPath    = "certificate.pfx";   // signing certificate
        const string pfxPassword = "password";         // certificate password

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

        // Load the document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // ------------------------------------------------------------
            // 1. Create a signature field (required) on page 1
            // ------------------------------------------------------------
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                Name = "Signature1",   // field name
                Required = true       // make the field required
            };
            // Add the field to the document's form collection
            doc.Form.Add(sigField);

            // ------------------------------------------------------------
            // 2. Prepare the digital signature (PKCS#7) using the certificate
            // ------------------------------------------------------------
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword)
            {
                Reason      = "Document approved",
                ContactInfo = "contact@example.com",
                Location    = "Head Office"
            };

            // ------------------------------------------------------------
            // 3. Sign the field
            // ------------------------------------------------------------
            sigField.Sign(pkcs7Signature);

            // ------------------------------------------------------------
            // 4. Lock the document after signing
            // ------------------------------------------------------------
            // Prevent further modifications by allowing only incremental updates
            doc.Form.SignaturesAppendOnly = true;
            // Throw an exception if an attempt is made to save changes after signing
            doc.HandleSignatureChange = true;

            // ------------------------------------------------------------
            // 5. Save the signed and locked PDF
            // ------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and locked PDF saved to '{outputPdf}'.");
    }
}