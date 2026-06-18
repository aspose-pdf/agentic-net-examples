using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security; // Note: Do NOT add a using directive for this namespace; we will use fully qualified names instead.

class Program
{
    static void Main()
    {
        const string outputPdf = "signed_form.pdf";
        const string pfxPath   = "certificate.pfx";
        const string pfxPassword = "pfxPassword";

        // Ensure the certificate file exists
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page where the signature field will be placed
            Page page = doc.Pages.Add();

            // Define the rectangle for the signature field (left, bottom, right, top)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field on the page
            SignatureField sigField = new SignatureField(page, sigRect)
            {
                // Optional: set a tooltip (alternate name) for the field
                AlternateName = "Digital Signature"
            };

            // Add the signature field to the document's form
            doc.Form.Add(sigField);

            // Load the certificate (PFX) as a stream and create a concrete PKCS7 signature object
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                // PKCS7 is a concrete implementation of the abstract Signature class
                PKCS7 pkcs7 = new PKCS7(pfxStream, pfxPassword)
                {
                    Reason = "Document approved",
                    Location = "Office",
                    ContactInfo = "contact@example.com"
                };

                // Sign the document using the signature field
                sigField.Sign(pkcs7);
            }

            // OPTIONAL: Validate the signature after signing
            Aspose.Pdf.Security.ValidationOptions validationOptions = new Aspose.Pdf.Security.ValidationOptions
            {
                // Use strict validation so that certificate chain issues invalidate the signature
                ValidationMode = Aspose.Pdf.Security.ValidationMode.Strict,
                // Enable certificate chain checking
                CheckCertificateChain = true
            };

            // Verify the signature; the Verify method returns true if the signature is valid
            bool isValid = sigField.Signature.Verify(validationOptions, out var validationResult);

            Console.WriteLine($"Signature valid: {isValid}");
            if (!isValid && validationResult != null)
            {
                Console.WriteLine($"Validation error: {validationResult.Message}");
            }

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
