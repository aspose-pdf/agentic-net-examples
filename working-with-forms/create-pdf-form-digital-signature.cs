using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string outputPdf = "signed_form.pdf";
        const string pfxPath   = "certificate.pfx";
        const string pfxPass   = "pfxPassword";

        // Ensure the certificate file exists
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle for the signature field (left, bottom, width, height)
            // Aspose.Pdf.Rectangle constructor: (llx, lly, urx, ury)
            // Here we calculate upper‑right coordinates from left, bottom, width, height.
            double left = 100, bottom = 500, width = 200, height = 50;
            var sigRect = new Rectangle(left, bottom, left + width, bottom + height);

            // Create the signature field on the page
            var sigField = new SignatureField(page, sigRect)
            {
                Name = "Signature1",
                AlternateName = "Sign Here",
                Color = Color.LightGray
                // Border styling can be added via the Border property if needed.
            };

            // Add the signature field to the document's form
            doc.Form.Add(sigField);

            // Sign the document using the PFX certificate (field‑level signing)
            // Use the PKCS7 class – Signature is abstract and cannot be instantiated directly.
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPass);
            // Optional: set reason, location, contact info on the PKCS7 object if desired.
            // pkcs7.Reason = "Document approval";
            // pkcs7.Location = "New York";
            // pkcs7.ContactInfo = "signer@example.com";

            // Apply the signature to the field
            sigField.Sign(pkcs7);

            // Save the signed PDF (PDF format is default)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
