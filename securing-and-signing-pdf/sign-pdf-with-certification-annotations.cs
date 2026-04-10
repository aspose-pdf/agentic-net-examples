using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // PDF to be signed
        const string outputPdfPath  = "signed.pdf";         // Resulting signed PDF
        const string pfxPath        = "certificate.pfx";   // PKCS#12 certificate file
        const string pfxPassword    = "password";           // Certificate password

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will appear (float values are required)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a signature field on the first page and add it to the page annotations
            SignatureField sigField = new SignatureField(doc, sigRect);
            doc.Pages[1].Annotations.Add(sigField);

            // Create a concrete PKCS#7 signature object (Signature is abstract)
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason      = "Document certified",
                Location    = "Company",
                ContactInfo = "contact@example.com",
                Date        = DateTime.Now
            };

            // Sign the field using the PKCS#7 signature (certification via DocMDP is not available in core API)
            sigField.Sign(pkcs7);

            // Ensure the signature is stored as an incremental update (required for any certification‑like behaviour)
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed PDF (lifecycle rule: use Save within using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF successfully signed and saved to '{outputPdfPath}'.");
    }
}
