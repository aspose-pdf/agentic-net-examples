using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // PDF containing the 'ClientSignature' field
        const string outputPdfPath = "signed_locked.pdf"; // Resulting PDF
        const string pfxPath       = "certificate.pfx";   // PKCS#12 certificate file
        const string pfxPassword   = "password";          // Certificate password

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Retrieve the signature field named 'ClientSignature'
            // The Form collection returns a generic Field; cast to SignatureField
            SignatureField sigField = doc.Form["ClientSignature"] as SignatureField;
            if (sigField == null)
            {
                Console.Error.WriteLine("Signature field 'ClientSignature' not found.");
                return;
            }

            // Create a PKCS#7 signature object using the certificate
            // Signature(Stream, string) constructor is used to avoid file path issues
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                PKCS7 pkcs7Signature = new PKCS7(pfxStream, pfxPassword);

                // Optional: set signature appearance properties
                pkcs7Signature.Reason   = "Approved";
                pkcs7Signature.Location = "Company HQ";
                pkcs7Signature.ContactInfo = "contact@example.com";

                // Sign the field
                sigField.Sign(pkcs7Signature);
            }

            // Lock the signature field to prevent further edits
            sigField.ReadOnly = true;

            // Optional: enforce that any further changes to the document raise an exception
            // (prevents accidental modifications after signing)
            doc.HandleSignatureChange = true;

            // Save the signed and locked PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Document signed and field locked. Saved to '{outputPdfPath}'.");
    }
}