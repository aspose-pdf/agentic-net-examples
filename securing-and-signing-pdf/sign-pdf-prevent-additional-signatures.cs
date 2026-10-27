using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the output PDF, and the signing certificate (PFX)
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_final.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "certPassword";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
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
            Document pdfDocument = new Document(inputPdfPath);

            // Define a rectangle for the signature field. A zero‑size rectangle makes the signature invisible.
            Rectangle signatureRect = new Rectangle(0, 0, 0, 0);

            // Create a signature field on the first page
            SignatureField signatureField = new SignatureField(pdfDocument.Pages[1], signatureRect)
            {
                PartialName = "Signature1"
            };

            // Add the signature field to the document's form collection (page 1)
            pdfDocument.Form.Add(signatureField, 1);

            // Configure basic signature information (visual appearance is not required for an invisible signature)
            signatureField.Signature.ContactInfo = "contact@example.com";
            signatureField.Signature.Location = "Company Headquarters";
            signatureField.Signature.Reason = "Document approved and finalized";

            // NOTE: The current Aspose.Pdf version used in this project does not expose the
            //       CertificationLevel or IsVisible properties on the Signature object.
            //       The zero‑size rectangle already makes the signature invisible, and the
            //       certification level (to block further changes) is not available.
            //       If you need a certified signature, upgrade to a newer Aspose.Pdf version
            //       that includes the CertificationLevel enum.

            // Create a PKCS#7 object from the certificate file
            PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword);

            // Apply the digital signature. In the current API the Sign method is available
            // directly on the SignatureField instance.
            signatureField.Sign(pkcs7);

            // Save the signed PDF to the desired output location
            pdfDocument.Save(outputPdfPath);

            Console.WriteLine($"PDF signed successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}
