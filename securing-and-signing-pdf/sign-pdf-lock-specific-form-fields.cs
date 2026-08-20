using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_locked.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Ensure a signature field exists (create if missing)
            const string sigFieldName = "Signature1";
            if (!form.HasField(sigFieldName))
            {
                // Define rectangle for the signature field on page 1
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                // Create and add the signature field
                SignatureField sigField = new SignatureField(doc, sigRect)
                {
                    Name = sigFieldName,
                    // Make the field read‑only after signing (will be enforced by the signature)
                    ReadOnly = true
                };
                form.Add(sigField);
            }

            // Retrieve the signature field
            SignatureField signatureField = (SignatureField)form[sigFieldName];

            // Create a concrete PKCS#7 signature (attached) using the certificate
            PKCS7 pkcs7Signature = new PKCS7(certPath, certPassword)
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the document using the signature field
            signatureField.Sign(pkcs7Signature);

            // Lock specific form fields after signing
            string[] fieldsToLock = { "Field1", "Field2", "Field3" };
            foreach (string fieldName in fieldsToLock)
            {
                if (form.HasField(fieldName))
                {
                    // Set the field to read‑only to prevent user edits
                    form[fieldName].ReadOnly = true;
                }
            }

            // Optional: prevent any further changes that would invalidate the signature
            form.SignaturesAppendOnly = true;

            // Save the modified PDF (lifecycle rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
