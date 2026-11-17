using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_sha384.pdf";
        const string certPath = "certificate.pfx";
        const string certPass = "certPassword";

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
            // Define the rectangle where the visible signature will appear
            Rectangle rect = new Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page and add it to the form
            SignatureField sigField = new SignatureField(doc.Pages[1], rect);
            doc.Form.Add(sigField);

            // Create PKCS7 credentials (concrete implementation of the abstract Signature class)
            PKCS7 pkcs = new PKCS7(certPath, certPass);
            // pkcs.Name property does not exist – it is omitted.
            pkcs.Location = "New York, USA";
            pkcs.Reason = "Document approval";
            pkcs.ContactInfo = "john.doe@example.com";

            // Configure SHA‑384 as the digest algorithm via TimestampSettings
            pkcs.TimestampSettings = new Aspose.Pdf.TimestampSettings(
                string.Empty,               // No TSA server URL (optional)
                null,                       // No basic‑auth credentials
                Aspose.Pdf.DigestHashAlgorithm.Sha384);

            // Sign the field with the PKCS7 credentials
            sigField.Sign(pkcs);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}' using SHA‑384 digest.");
    }
}
