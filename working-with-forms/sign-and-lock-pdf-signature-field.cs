using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";          // source PDF
        const string outputPath  = "signed_locked.pdf";  // result PDF
        const string certPath    = "certificate.pfx";    // signing certificate
        const string certPassword = "password";          // certificate password

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the signature field named "ClientSignature"
            SignatureField sigField = doc.Form["ClientSignature"] as SignatureField;
            if (sigField == null)
            {
                Console.Error.WriteLine("Signature field 'ClientSignature' not found.");
                return;
            }

            // Create a PKCS#7 signature object using the certificate
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason   = "Approved",
                Location = "Company HQ"
            };

            // Sign the field
            sigField.Sign(pkcs7);

            // Lock the field to prevent further modifications
            sigField.ReadOnly = true;

            // Optional: enforce that any further changes that would affect signatures raise an exception
            doc.EnableSignatureSanitization = false;
            doc.HandleSignatureChange = true;

            // Save the signed and locked PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed and locked PDF saved to '{outputPath}'.");
    }
}