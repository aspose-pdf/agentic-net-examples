using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF with form fields
        const string outputPdf  = "signed_locked.pdf"; // result PDF
        const string certPath   = "certificate.pfx";   // signing certificate
        const string certPass   = "password";          // certificate password

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // 1. Locate the signature field (assumed to exist)
            // -------------------------------------------------
            const string signatureFieldName = "Signature1";
            if (!doc.Form.HasField(signatureFieldName))
            {
                Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found.");
                return;
            }

            SignatureField sigField = (SignatureField)doc.Form[signatureFieldName];

            // -------------------------------------------------
            // 2. Create a concrete Signature object (PKCS7) and sign the field
            // -------------------------------------------------
            PKCS7 pkcs7 = new PKCS7(certPath, certPass);
            // Optional: set additional signature properties
            pkcs7.Reason = "Document approved";
            pkcs7.Location = "Office";
            pkcs7.ContactInfo = "contact@example.com";

            sigField.Sign(pkcs7);

            // -------------------------------------------------
            // 3. Lock specific form fields after signing
            // -------------------------------------------------
            string[] fieldsToLock = { "Field1", "Field2", "Field3" };
            foreach (string fieldName in fieldsToLock)
            {
                if (doc.Form.HasField(fieldName))
                {
                    // The Field base class provides the ReadOnly property
                    doc.Form[fieldName].ReadOnly = true;
                }
                else
                {
                    Console.WriteLine($"Warning: field '{fieldName}' not found.");
                }
            }

            // -------------------------------------------------
            // 4. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF with locked fields saved to '{outputPdf}'.");
    }
}
