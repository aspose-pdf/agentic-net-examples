using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;          // For SignatureField, PKCS7, Signature
using Aspose.Pdf.Drawing;        // For Rectangle (Aspose.Pdf.Rectangle)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdfPath = "SignedForm.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        // Verify that the certificate file exists
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the signature field will appear
            // (llx, lly, urx, ury) – lower‑left and upper‑right coordinates
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field and add it to the document's form collection
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                PartialName = "Signature1",   // field name
                // Optional: tooltip shown in PDF viewers
                AlternateName = "Please sign here"
            };
            doc.Form.Add(sigField);

            // Prepare the digital signature using a PKCS#7 certificate
            PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword)
            {
                Reason = "Document approval",
                ContactInfo = "signer@example.com",
                Location = "New York"
            };

            // Sign the field
            sigField.Sign(pkcs7Signature);

            // Verify the signature immediately after signing
            bool isValid = pkcs7Signature.Verify();
            Console.WriteLine($"Signature verification result: {isValid}");

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}