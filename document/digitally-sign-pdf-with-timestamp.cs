using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";
        const string tsaUrl = "https://freetsa.org/tsr"; // Trusted Time‑Stamp Authority URL
        const string tsaCredentials = "user:pass";      // "username:password"

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

        // Load the PDF document (lifecycle rule: using block)
        using (Document doc = new Document(inputPath))
        {
            // Create a signature field on the first page
            // Fully qualified Rectangle avoids ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc.Pages[1], rect);
            doc.Form.Add(sigField);

            // Create a PKCS#7 signature using the PFX certificate
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword);
            pkcs7.Reason = "Approved";
            pkcs7.Location = "Office";
            pkcs7.ContactInfo = "contact@example.com";

            // Attach timestamp settings from a trusted TSA
            pkcs7.TimestampSettings = new TimestampSettings(
                tsaUrl,
                tsaCredentials,
                DigestHashAlgorithm.Sha256);

            // Sign the field with the configured signature
            sigField.Sign(pkcs7);

            // Save the signed PDF (lifecycle rule: using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}