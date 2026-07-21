using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_output.pdf"; // signed PDF
        const string certPath   = "certificate.pfx";   // signing certificate
        const string certPass   = "pfxPassword";       // certificate password

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has a form (required for signature fields)
            Form form = doc.Form;

            // Add a signature field on the first page if none exists
            // Rectangle: left, bottom, width, height
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 200, 100);
            SignatureField sigField = new SignatureField(doc, sigRect);
            sigField.PartialName = "Signature1";
            sigField.AlternateName = "Signature Field";
            sigField.Color = Aspose.Pdf.Color.LightGray; // optional visual cue
            form.Add(sigField, 1); // add to page 1 (1‑based indexing)

            // Create a PKCS#7 signature object using the certificate
            PKCS7 pkcs7 = new PKCS7(certPath, certPass)
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com",
                Date = DateTime.UtcNow
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7);

            // Lock the signature field to prevent further edits
            sigField.ReadOnly = true;

            // Prevent any further modifications to existing signatures
            // (incremental updates that would invalidate signatures)
            form.SignaturesAppendOnly = true;

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}