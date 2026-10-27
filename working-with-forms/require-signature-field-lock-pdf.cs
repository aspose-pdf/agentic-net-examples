using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has a form (AcroForm)
                Form form = doc.Form;

                // Define the rectangle where the signature field will appear (llx, lly, urx, ury)
                Rectangle rect = new Rectangle(100, 500, 300, 550);

                // Create a signature field on page 1
                SignatureField sigField = new SignatureField(doc.Pages[1], rect)
                {
                    PartialName = "Signature1",
                    Required = true // make the field required
                };

                // Add the signature field to the form on page 1
                form.Add(sigField, 1);

                // Create a PKCS#7 signature using the certificate file
                PKCS7 pkcs = new PKCS7(certPath, certPassword)
                {
                    Reason = "Document approved",
                    Location = "Office",
                    ContactInfo = "signer@example.com"
                };

                // Sign the field (Signature property is read‑only, so we use Sign())
                sigField.Sign(pkcs);

                // After a signature is applied, lock the document for further changes
                // This is achieved by enabling the Append‑Only mode for signatures.
                form.SignaturesAppendOnly = true;

                // Save the signed PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
