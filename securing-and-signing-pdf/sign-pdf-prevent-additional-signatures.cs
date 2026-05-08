using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Forms; // for PKCS7 signature class

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_final.pdf";
        const string pfxPath   = "certificate.pfx";
        const string pfxPassword = "password";

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a visible signature field on page 1
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);
            SignatureField sigField = new SignatureField(doc, sigRect);
            sigField.PartialName = "Signature1";
            doc.Form.Add(sigField);

            // Prepare the PKCS#7 signature object
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason   = "Document approved",
                ContactInfo = "signer@example.com",
                Location = "New York"
            };

            // Sign the field
            sigField.Sign(pkcs7);

            // Disallow further signatures by making the document append‑only.
            // This prevents additional signatures unless the file is saved incrementally.
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed and locked: {outputPdf}");
    }
}