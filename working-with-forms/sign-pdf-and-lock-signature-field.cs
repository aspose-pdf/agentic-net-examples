using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string finalPdf = "final.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "certPassword";

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
        Document doc = new Document(inputPdf);

        // Locate the signature field named "ClientSignature"
        var sigField = doc.Form["ClientSignature"] as SignatureField;
        if (sigField == null)
        {
            Console.Error.WriteLine("Signature field 'ClientSignature' not found.");
            return;
        }

        // Create a PKCS#7 signature using the provided certificate
        PKCS7 pkcs = new PKCS7(certPath, certPassword)
        {
            Reason = "Signed by client",
            ContactInfo = "Company",
            Location = "Document Review"
        };

        // Apply the digital signature to the field
        sigField.Sign(pkcs);

        // Lock the field to prevent any further modifications
        sigField.ReadOnly = true;

        // Save the signed and locked PDF
        doc.Save(finalPdf);

        Console.WriteLine($"Processing complete. Output saved to '{finalPdf}'.");
    }
}
