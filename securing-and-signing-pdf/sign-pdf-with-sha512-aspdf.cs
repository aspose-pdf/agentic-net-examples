using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, and certificate (PFX) paths
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "pfxPassword";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Create a signature field on the first page
                // Rectangle constructor: (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
                SignatureField signatureField = new SignatureField(pdfDocument.Pages[1], sigRect)
                {
                    // Assign a unique name to the field
                    PartialName = "Signature1"
                };

                // Add the signature field to the document's form collection
                pdfDocument.Form.Add(signatureField);

                // Load the signing certificate (contains private key)
                X509Certificate2 signingCert = new X509Certificate2(certificatePath, certificatePassword);

                // Create an external signature using SHA‑512 digest algorithm
                ExternalSignature externalSignature = new ExternalSignature(signingCert, DigestHashAlgorithm.Sha512)
                {
                    // Optional: set additional signature properties
                    Reason      = "Document approved",
                    ContactInfo = "contact@example.com",
                    Location    = "New York"
                };

                // Sign the field with the external signature
                signatureField.Sign(externalSignature);

                // Save the signed PDF
                pdfDocument.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}