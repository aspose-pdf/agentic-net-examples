using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_locked.pdf"; // result PDF
        const string certPath   = "certificate.pfx";   // signing certificate
        const string certPass   = "password";          // certificate password

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

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page (using 1‑based page indexing)
            // Rectangle: llx, lly, urx, ury
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc, rect)
            {
                // Make the field required before signing
                Required = true,
                // Optional: give the field a name
                PartialName = "Signature1"
            };
            // Add the field to the document's form
            doc.Form.Add(sigField);

            // Prepare the digital signature object (PKCS#7)
            PKCS7 pkcs7 = new PKCS7(certPath, certPass)
            {
                // Optional metadata
                Authority   = "John Doe",
                Location    = "New York, USA",
                Reason      = "Document approval",
                ContactInfo = "john.doe@example.com",
                Date        = DateTime.UtcNow
            };

            // Sign the field
            sigField.Sign(pkcs7);

            // Lock the document after signing:
            // 1. Prevent any further modifications by enabling signature change handling.
            // 2. Optionally, enforce incremental updates only.
            doc.HandleSignatureChange = true;          // throws if the document is saved with changes after signing
            doc.Form.SignaturesAppendOnly = true;      // ensures only incremental updates are allowed

            // Save the signed and locked PDF (using rule: save-to-non-pdf-always-use-save-options is not needed for PDF)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document signed, required field set, and locked: {outputPdf}");
    }
}