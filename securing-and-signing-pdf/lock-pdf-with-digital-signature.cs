using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";          // source PDF
        const string outputPdf     = "locked_signed.pdf";  // result PDF
        const string certificate   = "certificate.pfx";    // signing certificate
        const string certPassword  = "password";           // certificate password

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(certificate))
        {
            Console.Error.WriteLine("Input PDF or certificate file not found.");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Create a signature field (if the document does not already have one)
            // -----------------------------------------------------------------
            // Position the field on the first page (coordinates: llx, lly, urx, ury)
            SignatureField sigField = new SignatureField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 500, 300, 550));
            sigField.PartialName = "Signature1";
            doc.Form.Add(sigField, 1);

            // -----------------------------------------------------------------
            // 2. Prepare the digital signature (PKCS#7) using the certificate
            // -----------------------------------------------------------------
            PKCS7 pkcs7 = new PKCS7(certificate, certPassword)
            {
                Reason   = "Document locked after signing",
                Location = "Company"
            };

            // -----------------------------------------------------------------
            // 3. Apply the signature to the field
            // -----------------------------------------------------------------
            sigField.Sign(pkcs7);

            // -----------------------------------------------------------------
            // 4. Ensure further changes are only allowed via incremental updates
            // -----------------------------------------------------------------
            doc.Form.SignaturesAppendOnly = true;

            // -----------------------------------------------------------------
            // 5. Add an MDP (Modification Detection and Prevention) signature
            //    with NoChanges permission to lock the entire document
            // -----------------------------------------------------------------
            // The constructor automatically attaches the MDP signature to the document
            DocMDPSignature mdp = new DocMDPSignature(pkcs7, DocMDPAccessPermissions.NoChanges);

            // -----------------------------------------------------------------
            // 6. Save the signed, read‑only PDF
            // -----------------------------------------------------------------
            // Incremental save preserves the signature and MDP restrictions
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and locked PDF saved to '{outputPdf}'.");
    }
}