using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_locked.pdf";
        const string pfxPath    = "certificate.pfx";
        const string pfxPassword = "password";
        const string signatureFieldName = "ClientSignature";

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
            // Retrieve the signature field by name
            SignatureField sigField = doc.Form[signatureFieldName] as SignatureField;
            if (sigField == null)
            {
                Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found.");
                return;
            }

            // Create a PKCS#7 signature object
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword)
            {
                Reason      = "Approved by client",
                ContactInfo = "client@example.com",
                Location    = "Client Office"
            };

            // Sign the field
            sigField.Sign(pkcs7Signature);

            // Lock the signature field to prevent further edits
            sigField.ReadOnly = true;

            // Ensure that any subsequent changes invalidate the signature
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed and locked PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document signed and locked: {outputPdf}");
    }
}