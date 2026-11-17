using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_with_new_page.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "pfxPassword";

        // Verify files exist
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
            // Open the source PDF – wrap in using for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Create a signature field on the first page
                // Rectangle coordinates are (llx, lly, urx, ury) in points
                SignatureField signatureField = new SignatureField(
                    doc.Pages[1],
                    new Rectangle(100, 600, 300, 650));

                // Set a unique name for the field
                signatureField.PartialName = "CertSignature";

                // Add the signature field to the document's AcroForm
                doc.Form.Add(signatureField);

                // Create a PKCS#7 signature using a PFX certificate
                PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword);
                pkcs7.ContactInfo = "contact@example.com";
                pkcs7.Location = "Head Office";
                pkcs7.Reason = "Document certification – new pages may be added";

                // Sign the field (Signature property is read‑only, so we call Sign)
                signatureField.Sign(pkcs7);

                // Add a new blank page – allowed because we did not set any restrictive certification
                doc.Pages.Add();

                // Save the signed PDF (the signature is applied at save time)
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF signed and new page added successfully: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
