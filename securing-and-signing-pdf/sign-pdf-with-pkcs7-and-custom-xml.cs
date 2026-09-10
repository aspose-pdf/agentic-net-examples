using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "signed_output.pdf";  // signed PDF
        const string pfxPath   = "certificate.pfx";   // PKCS#12 certificate file
        const string pfxPass   = "password";          // certificate password

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature will appear (coordinates are in points)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on page 1
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                PartialName = "Signature1"   // field name
            };
            // Add the signature field to the document (page index is 1‑based)
            doc.Form.Add(sigField, 1);

            // Create a PKCS#7 signature object using the certificate
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPass)
            {
                Reason      = "Document approved",          // reason for signing
                ContactInfo = "contact@example.com",        // contact information
                Location    = "New York"                    // signing location
            };

            // OPTIONAL: embed custom XML signature data.
            // The CustomSignHash delegate can be used to provide a custom signing routine.
            // Here we illustrate a placeholder that could embed XML; replace with real logic as needed.
            // pkcs7.CustomSignHash = (hash) =>
            // {
            //     // Generate custom XML signature based on the hash and return the signature bytes.
            //     // For demonstration we simply return the original hash.
            //     return hash;
            // };

            // Sign the document using the signature field and the PKCS#7 object
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdf}'.");
    }
}