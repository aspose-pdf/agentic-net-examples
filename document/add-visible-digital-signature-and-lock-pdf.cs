using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "input.pdf";          // Existing PDF (or create a new one)
        const string outputPdf = "signed_output.pdf";
        const string pfxPath   = "certificate.pfx";   // PKCS#12 certificate file
        const string pfxPass   = "password";          // Certificate password

        // Ensure the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the visible signature will appear
            // (llx, lly, urx, ury) – coordinates are in points
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a signature field on the first page (page index is 1‑based)
            SignatureField sigField = new SignatureField(doc, sigRect);
            sigField.Name = "Signature1";               // Optional: give the field a name
            sigField.ReadOnly = false;                  // Initially editable so we can sign it

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // Prepare the digital signature object (PKCS#1 in this example)
            PKCS1 signature = new PKCS1(pfxPath, pfxPass);
            signature.Reason   = "Document approved";
            signature.ContactInfo = "john.doe@example.com";
            signature.Location = "New York";

            // Sign the field – this also makes the appearance visible (ShowProperties is true by default)
            sigField.Sign(signature);

            // Lock the signature field to prevent further changes
            sigField.ReadOnly = true;

            // Optional: enforce append‑only mode for the whole document (prevents modifications that would invalidate signatures)
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}