using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // PDF to be signed
        const string outputPdf  = "signed.pdf";     // Resulting PDF
        const string pfxPath    = "certificate.pfx"; // PFX containing signing certificate
        const string pfxPassword = "password";      // Password for the PFX

        // Verify required files exist
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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Allow incremental signatures so other users can add signatures later
            doc.Form.SignaturesAppendOnly = true;

            // Define the rectangle where the signature field will appear
            // (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            // Add the field to the document's form on page 1 (pages are 1‑based)
            doc.Form.Add(sigField, 1);

            // Create a PKCS#7 signature object using the certificate
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason   = "Approved",
                Location = "Head Office"
            };

            // Sign the document using the created signature field
            sigField.Sign(pkcs7);

            // Save the signed PDF (default Save writes an incremental update,
            // preserving the ability to add further signatures)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdf}'.");
    }
}