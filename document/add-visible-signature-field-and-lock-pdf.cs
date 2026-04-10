using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF
        const string outputPath = "signed_output.pdf"; // signed PDF
        const string pfxPath = "certificate.pfx";      // signing certificate
        const string pfxPassword = "password";        // certificate password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Create a visible signature field on page 1
            // Rectangle: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc, rect)
            {
                PartialName = "Signature1" // field name
            };

            // Add the field to the form (page 1)
            doc.Form.Add(sigField, 1);

            // Add a visible appearance for the field (instance method on the Form object)
            doc.Form.AddFieldAppearance(sigField, 1, rect);

            // Create a concrete PKCS7 signature object from the PFX certificate
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the field (visible appearance already added)
            sigField.Sign(pkcs7);

            // Lock the document after signing (prevent further modifications)
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
