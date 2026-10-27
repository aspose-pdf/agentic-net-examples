using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – replace with actual file locations
        const string inputPdfPath   = "input.pdf";
        const string finalPdfPath   = "final.pdf";
        const string certPath       = "certificate.pfx";
        const string certPassword   = "certPassword";
        const string signatureFieldName = "ClientSignature";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Retrieve the signature field by its partial name
            SignatureField sigField = doc.Form[signatureFieldName] as SignatureField;
            if (sigField == null)
            {
                Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found.");
                return;
            }

            // Create a PKCS#7 signature using the provided certificate
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Document signed by client",
                Location = "Company HQ",
                ContactInfo = "contact@example.com"
            };

            // Sign the field
            sigField.Sign(pkcs7);

            // Lock the field to prevent further modifications
            sigField.ReadOnly = true;

            // Save the final PDF with the locked signature field
            doc.Save(finalPdfPath);
        }

        Console.WriteLine($"Document processed. Final file: {finalPdfPath}");
    }
}
