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

        // Verify required files exist
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

        // Load the PDF document (lifecycle: using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the signature field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the document
            SignatureField sigField = new SignatureField(doc, rect)
            {
                PartialName = "Signature1" // optional field name
            };

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // Create a PKCS#7 signature object using the certificate (concrete implementation of abstract Signature)
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7);

            // Flatten all form fields so they become part of the page content
            doc.Flatten();

            // Save the signed and flattened PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPath}'.");
    }
}
