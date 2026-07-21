using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the output PDF, the certificate (PFX) and its password
        const string outputPdfPath = "signed_form.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "pfxPassword";

        // Ensure the certificate file exists
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Create a new PDF document (empty) and add a page
        using (Document doc = new Document())
        {
            // Add a blank page to host the signature field
            Page page = doc.Pages.Add();

            // Define the rectangle for the signature field (left, bottom, right, top)
            var sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field on the page
            var sigField = new SignatureField(page, sigRect)
            {
                Name = "Signature1",               // field name
                AlternateName = "Sign Here",       // tooltip
                Required = true,                    // make it required
                ReadOnly = false                    // allow signing
            };

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // Use the core‑API PKCS1 class for signing (no Facades, no Aspose.Pdf.Signature namespace)
            var pkcs1 = new PKCS1(pfxPath, pfxPassword)
            {
                Reason = "Document approval",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs1);

            // Verify the signature – simple verification that returns true if the signature is cryptographically valid
            bool isValid = sigField.Signature.Verify();

            Console.WriteLine($"Signature verification result: {(isValid ? "Valid" : "Invalid")}");

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
