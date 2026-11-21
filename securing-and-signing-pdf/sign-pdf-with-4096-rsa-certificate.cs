using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class SignPdf
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";   // RSA 4096‑bit private key in PKCS#12 container
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

        // Open the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a visible signature field on the first page
            var signatureRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);
            var signatureField = new SignatureField(pdfDoc.Pages[1], signatureRect)
            {
                PartialName = "Signature1"
            };

            // Add the signature field to the document's form collection
            pdfDoc.Form.Add(signatureField);

            // Create a PKCS#7 signature using the loaded certificate (SHA‑256 hash algorithm)
            var pkcs7 = new PKCS7(certificatePath, certificatePassword)
            {
                Reason = "Document approval",
                Location = "New York, USA",
                ContactInfo = "contact@example.com"
            };

            // Sign the field – the Signature property is read‑only, so we invoke Sign()
            signatureField.Sign(pkcs7);

            // Save the signed PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}
