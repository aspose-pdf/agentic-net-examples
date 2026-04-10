using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdf = "signed_form.pdf";
        const string certPath = "certificate.pfx";
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
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle for the signature field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field on the page
            SignatureField sigField = new SignatureField(page, sigRect)
            {
                PartialName = "Signature1",          // field name
                AlternateName = "Sign Here",        // tooltip
                Color = Aspose.Pdf.Color.LightGray // optional visual cue
            };

            // Add the field to the page annotations collection
            page.Annotations.Add(sigField);

            // Create a PKCS#1 signature object using the PFX certificate
            PKCS1 pkcs1Signature = new PKCS1(certPath, certPassword)
            {
                Reason = "Document approval",
                ContactInfo = "contact@example.com",
                Location = "New York, USA"
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs1Signature);

            // Verify the signature immediately (optional)
            bool isValid = pkcs1Signature.Verify();
            Console.WriteLine($"Signature verification result (in‑memory): {isValid}");

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        // Load the saved PDF to verify the signature from the file
        using (Document signedDoc = new Document(outputPdf))
        {
            // Retrieve the signature field by name from the form
            // The Form property provides access to AcroForm fields
            SignatureField loadedField = signedDoc.Form["Signature1"] as SignatureField;
            if (loadedField != null && loadedField.Signature != null)
            {
                // Verify the signature using the embedded certificate
                bool isValid = loadedField.Signature.Verify();
                Console.WriteLine($"Signature verification result (saved file): {isValid}");
            }
            else
            {
                Console.WriteLine("Signature field not found or not signed.");
            }
        }
    }
}