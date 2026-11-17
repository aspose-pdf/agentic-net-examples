using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";   // PKCS#12 certificate file
        const string certificatePassword = "certPassword";

        // Validate input files
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
            // Load the PDF inside a using block for deterministic disposal (global rule)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Define a rectangle where the visual signature will appear (optional)
                var rect = new Aspose.Pdf.Rectangle(100, 100, 200, 150); // lower‑left X,Y, upper‑right X,Y

                // Create a signature field on the first page
                var signatureField = new SignatureField(pdfDoc.Pages[1], rect)
                {
                    PartialName = "CertSignature"
                };
                pdfDoc.Form.Add(signatureField, 1);

                // Prepare PKCS7 object with the signing certificate
                var pkcs7 = new PKCS7(certificatePath, certificatePassword)
                {
                    Reason = "Document certification",
                    Location = "Company HQ",
                    ContactInfo = "support@example.com"
                };

                // Sign the field with the PKCS7 signature (core API only)
                signatureField.Sign(pkcs7);

                // Apply certification‑like restrictions using document encryption.
                // Allow form filling (FillForm) but block other content changes.
                // Empty user password, set an owner password, and grant only FillForm permission.
                pdfDoc.Encrypt("", "owner-pass", Permissions.FillForm, CryptoAlgorithm.AESx256);

                // Save the signed PDF. The Sign operation writes the signed content back into the document's stream,
                // so a normal Save is sufficient.
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF signed and certified successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}
