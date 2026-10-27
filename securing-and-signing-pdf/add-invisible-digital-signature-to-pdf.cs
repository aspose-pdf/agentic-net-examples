using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create an invisible signature field (zero‑size rectangle).
            // A zero‑size rectangle makes the field invisible and does not affect layout.
            var invisibleRect = new Rectangle(0, 0, 0, 0);
            SignatureField sigField = new SignatureField(doc, invisibleRect)
            {
                PartialName = "Signature1" // required identifier for the field
            };

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // Create a PKCS#7 signature using the certificate file
            PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
            {
                Reason = "Document approved",
                Location = "Head Office",
                ContactInfo = "security@example.com"
                // Signing time is recorded automatically when Sign() is called
            };

            // Apply the digital signature to the field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Invisible digital signature applied. Output saved to '{outputPath}'.");
    }
}
