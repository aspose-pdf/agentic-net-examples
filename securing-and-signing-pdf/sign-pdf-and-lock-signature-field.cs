using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_locked.pdf"; // result PDF
        const string pfxPath    = "certificate.pfx";   // signing certificate
        const string pfxPassword = "pfxPassword";      // certificate password

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page
            // Fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                // Optional: give the field a name (used for identification)
                Name = "Signature1",
                // Make the field read‑only after signing
                ReadOnly = true
            };
            // Add the field to the page annotations collection
            doc.Pages[1].Annotations.Add(sigField);

            // Create a PKCS#7 signature object using the PFX file
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason   = "Document approved",
                Location = "Company HQ",
                ContactInfo = "contact@example.com",
                Date = DateTime.Now
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7);

            // Prevent any further modifications to the PDF after this signature
            // 1) Mark the document as containing append‑only signatures
            doc.Form.SignaturesAppendOnly = true;
            // 2) Optionally, add a document‑level MDP signature that disallows changes
            //    (requires Aspose.Pdf.Forms.DocMDPAccessPermissions enum)
            // DocMDPSignature mdpSig = new DocMDPSignature(pkcs7, DocMDPAccessPermissions.NoChanges);
            // doc.Signatures.Add(mdpSig); // Uncomment if the API version supports this

            // Save the signed and locked PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}