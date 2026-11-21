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
        const string outputPdfPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePwd = "certPassword";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate not found: {certificatePath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Create an invisible signature field on the first page (zero‑size rectangle)
                var invisibleRect = new Rectangle(0, 0, 0, 0);
                var signatureField = new SignatureField(pdfDoc.Pages[1], invisibleRect)
                {
                    PartialName = "Signature1"
                };

                // Add the signature field to the document's form (page 1)
                pdfDoc.Form.Add(signatureField, 1);

                // Prepare PKCS7 signature (digital signature)
                var pkcs7 = new PKCS7(certificatePath, certificatePwd)
                {
                    Reason = "Document approval",
                    Location = "Company HQ",
                    ContactInfo = "support@example.com"
                    // Signing time is automatically recorded; no SignDate property exists.
                };

                // Apply the signature to the invisible field
                signatureField.Sign(pkcs7);

                // Save the signed PDF (layout unchanged)
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
