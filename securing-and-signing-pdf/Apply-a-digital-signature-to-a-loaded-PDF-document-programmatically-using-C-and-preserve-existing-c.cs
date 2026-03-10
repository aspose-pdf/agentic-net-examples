using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // PDF to be signed
        const string outputPath = "signed_output.pdf";  // Resulting signed PDF
        const string pfxPath    = "certificate.pfx";   // PFX containing signing certificate
        const string pfxPassword = "password";         // Password for the PFX

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the signature field will appear
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the document (first page by default)
            SignatureField signatureField = new SignatureField(doc, rect);

            // Add the signature field to the document's form fields collection
            doc.Form.Add(signatureField);

            // Create a PKCS#7 signature object using the certificate file
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason      = "Document approved",
                Location    = "Company HQ",
                ContactInfo = "contact@example.com"
            };

            // Sign the document using the created signature field
            signatureField.Sign(pkcs7);

            // Save the signed PDF, preserving all existing content
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}