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
        const string certPath   = "certificate.pfx";   // signing certificate
        const string certPass   = "password";          // certificate password

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate not found: {certPath}");
            return;
        }

        // Load the document
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Create a signature field and mark it as required
            // -----------------------------------------------------------------
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                Required = true               // make the field required
            };
            // Add the field to the AcroForm
            doc.Form.Add(sigField);

            // -----------------------------------------------------------------
            // 2. Prepare the digital signature object (PKCS#7 in this example)
            // -----------------------------------------------------------------
            PKCS7 pkcs7 = new PKCS7(certPath, certPass)
            {
                Reason   = "Document approved",
                Location = "Head Office",
                // Optional: set appearance, timestamp, etc.
            };

            // -----------------------------------------------------------------
            // 3. Sign the document using the created field
            // -----------------------------------------------------------------
            sigField.Sign(pkcs7);

            // -----------------------------------------------------------------
            // 4. Lock the document after signing
            // -----------------------------------------------------------------
            // a) Prevent any further modifications by enabling MDP (no changes)
            DocMDPSignature mdpSig = new DocMDPSignature(pkcs7, DocMDPAccessPermissions.NoChanges);
            // Adding the MDP signature to the document forces the lock.
            // The MDP signature is attached automatically when the document is saved.
            // b) Additionally, make the API throw an exception if any change is attempted
            doc.HandleSignatureChange = true;

            // -----------------------------------------------------------------
            // 5. Save the signed and locked PDF
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and locked PDF saved to '{outputPdf}'.");
    }
}