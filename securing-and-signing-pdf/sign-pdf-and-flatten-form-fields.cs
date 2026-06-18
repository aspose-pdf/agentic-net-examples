using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_flattened.pdf";
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the signature field rectangle
            Rectangle rect = new Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, rect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField);

            // Create a concrete PKCS7 signature object (Signature is abstract)
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the signature field
            sigField.Sign(pkcs7);

            // Flatten all form fields to prevent further editing
            doc.Flatten();

            // Save the signed and flattened PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPath}'.");
    }
}
