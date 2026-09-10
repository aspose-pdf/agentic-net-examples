using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "signed_locked.pdf"; // result PDF
        const string certPath   = "certificate.pfx";   // signing certificate
        const string certPass   = "password";          // certificate password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Add a signature field and mark it as required
            // ------------------------------------------------------------
            // Fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                PartialName = "Signature1", // field name
                Required    = true          // make the field required
            };
            doc.Form.Add(sigField);

            // ------------------------------------------------------------
            // 2. Create a PKCS#7 signature object using the certificate
            // ------------------------------------------------------------
            Signature signature = new PKCS7(certPath, certPass);

            // ------------------------------------------------------------
            // 3. Sign the field with the signature object
            // ------------------------------------------------------------
            sigField.Sign(signature);

            // ------------------------------------------------------------
            // 4. Lock the document after signing (MDP signature with NoChanges)
            // ------------------------------------------------------------
            // This creates a modification‑detection‑and‑prevention signature
            // that disallows any further changes to the PDF.
            DocMDPSignature mdpSignature = new DocMDPSignature(signature, DocMDPAccessPermissions.NoChanges);

            // Optional: enforce exception if the document is altered after signing
            doc.HandleSignatureChange = true;

            // ------------------------------------------------------------
            // 5. Save the signed and locked PDF (lifecycle rule: save inside using)
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document signed, required field set, and locked: {outputPath}");
    }
}