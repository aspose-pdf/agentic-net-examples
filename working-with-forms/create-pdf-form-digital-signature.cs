using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdf = "SignedForm.pdf";
        const string certPath  = "certificate.pfx";
        const string certPassword = "password";

        // Ensure the certificate file exists
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle for the signature field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field on the page
            SignatureField sigField = new SignatureField(page, sigRect)
            {
                // Optional: set a name and tooltip (alternate name)
                Name = "Signature1",
                AlternateName = "Please sign here"
            };

            // Add the field to the page's annotations collection
            page.Annotations.Add(sigField);

            // Prepare the PKCS#7 signature object using the certificate
            PKCS7 pkcs7Signature = new PKCS7(certPath, certPassword)
            {
                Reason      = "Document approval",
                ContactInfo = "signer@example.com",
                Location    = "New York"
                // ShowProperties is true by default – it will display signer info in the appearance
            };

            // Sign the document using the created signature field
            sigField.Sign(pkcs7Signature);

            // OPTIONAL: Verify the signature immediately after signing
            bool isValid = sigField.Signature.Verify();
            Console.WriteLine($"Signature verification result: {isValid}");

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}