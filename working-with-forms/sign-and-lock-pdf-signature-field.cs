using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_locked.pdf";
        const string pfxPath = "cert.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPath) || !File.Exists(pfxPath))
        {
            Console.Error.WriteLine("Required input PDF or certificate file not found.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the signature field named 'ClientSignature'
            SignatureField sigField = doc.Form["ClientSignature"] as SignatureField;
            if (sigField == null)
            {
                Console.Error.WriteLine("Signature field 'ClientSignature' not found.");
                return;
            }

            // Create a PKCS#7 signature using the certificate
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Approved",
                Location = "Company HQ"
            };

            // Sign the field
            sigField.Sign(pkcs7);

            // Lock the field to prevent further modifications
            sigField.ReadOnly = true;

            // Optional: enforce signature integrity on subsequent saves
            doc.EnableSignatureSanitization = true;
            doc.HandleSignatureChange = true;

            // Save the signed and locked PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed and locked PDF saved to '{outputPath}'.");
    }
}