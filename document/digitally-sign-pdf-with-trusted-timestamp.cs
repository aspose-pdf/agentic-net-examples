using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_output.pdf"; // signed PDF
        const string pfxPath    = "certificate.pfx";   // signing certificate
        const string pfxPassword = "pfxPassword";      // certificate password

        // Trusted Time‑Stamp Authority (TSA) details
        const string tsaUrl      = "https://freetsa.org/tsr"; // example TSA URL
        const string tsaCreds    = "username:password";      // basic auth credentials

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

        // Load the PDF document (lifecycle rule: load)
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page
            Page page = doc.Pages[1];
            // Define the rectangle for the signature appearance
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            // Add the signature field annotation
            SignatureField signatureField = new SignatureField(page, rect);
            page.Annotations.Add(signatureField);

            // Configure timestamp settings (trusted TSA)
            TimestampSettings tsSettings = new TimestampSettings(
                serverUrl: tsaUrl,
                basicAuthCredentials: tsaCreds,
                digestHashAlgorithm: DigestHashAlgorithm.Sha256);

            // Create a PKCS#7 signature object using the certificate
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword);
            // Set optional signature properties
            pkcs7Signature.Reason   = "Document approved";
            pkcs7Signature.Location = "Head Office";
            pkcs7Signature.ContactInfo = "contact@example.com";
            // Attach the timestamp settings to the signature
            pkcs7Signature.TimestampSettings = tsSettings;

            // Sign the document using the signature field (lifecycle rule: sign)
            signatureField.Sign(pkcs7Signature);

            // Save the signed PDF (lifecycle rule: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}