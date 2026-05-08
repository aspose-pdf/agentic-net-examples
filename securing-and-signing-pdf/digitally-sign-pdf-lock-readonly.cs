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
        const string outputPdf  = "signed_locked.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // 1. Add a signature field to the first page
            // -------------------------------------------------
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField);

            // -------------------------------------------------
            // 2. Create a PKCS#7 signature object
            // -------------------------------------------------
            PKCS7 pkcs7Signature = new PKCS7(certPath, certPass)
            {
                Reason      = "Document approved",
                ContactInfo = "contact@example.com",
                Location    = "Office"
            };

            // -------------------------------------------------
            // 3. Sign the field with the PKCS#7 signature
            // -------------------------------------------------
            sigField.Sign(pkcs7Signature);

            // -------------------------------------------------
            // 4. Lock the PDF – encrypt with no permissions (read‑only)
            // -------------------------------------------------
            // Permissions.None is equivalent to 0 (no modifications allowed)
            Permissions noPermissions = (Permissions)0;
            doc.Encrypt(userPassword: "", ownerPassword: "", permissions: noPermissions, cryptoAlgorithm: CryptoAlgorithm.AESx256);

            // -------------------------------------------------
            // 5. Save the signed, locked PDF
            // -------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and locked PDF saved to '{outputPdf}'.");
    }
}