using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and certificate (PFX) paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string pfxPath   = "certificate.pfx";
        const string pfxPassword = "pfxPassword";

        // Ensure the input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page
            // Fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);
            SignatureField signatureField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            // Add the signature field to the document's form collection
            doc.Form.Add(signatureField, 1);

            // Create a PKCS#7 detached signature object that uses SHA‑512
            // DigestHashAlgorithm is in the Aspose.Pdf namespace
            PKCS7Detached pkcs7Signature = new PKCS7Detached(Aspose.Pdf.DigestHashAlgorithm.Sha512)
            {
                Reason   = "Document approved",
                Location = "Head Office",
                Date     = DateTime.UtcNow
            };

            // Sign the field using the certificate (PFX) and the SHA‑512 configured signature object
            using (FileStream pfxStream = new FileStream(pfxPath, FileMode.Open, FileAccess.Read))
            {
                signatureField.Sign(pkcs7Signature, pfxStream, pfxPassword);
            }

            // Save the signed PDF (wrapped in using ensures the document stays alive until Save completes)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed with SHA‑512 and saved to '{outputPdf}'.");
    }
}