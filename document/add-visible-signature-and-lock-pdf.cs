using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Signatures; // needed for PKCS7

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

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

        // Load the PDF (document‑disposal‑with‑using rule)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the visible signature field (double values)
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a visible signature field on the document
            var sigField = new SignatureField(doc, rect)
            {
                PartialName = "Signature1",               // field identifier
                Color = Aspose.Pdf.Color.Blue            // visible border color
            };

            // Add the field to the form (page number is optional here)
            doc.Form.Add(sigField);

            // Ensure the field has a visible appearance on page 1
            doc.Form.AddFieldAppearance(sigField, 1, rect);

            // Prepare a concrete signature implementation (PKCS7) – abstract Signature cannot be instantiated
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the field
            sigField.Sign(pkcs7);

            // Lock the field after signing to prevent further modifications
            // SignatureField does not expose a 'Locked' property; use the 'ReadOnly' flag instead.
            sigField.ReadOnly = true;

            // Optionally lock the whole form (if the Form class provides this property)
            // doc.Form.Locked = true; // uncomment if supported by the library version

            // Save the signed PDF (save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
