using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certificate = "certificate.pfx";
        const string certPassword = "certPass";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificate))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificate}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Define the rectangle where the signature will appear (lower‑left x/y, upper‑right x/y)
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

                // Create a signature field and add it to the form on the last page
                SignatureField sigField = new SignatureField(doc, sigRect)
                {
                    PartialName = "Signature1"
                };
                // The page number is 1‑based; add the field to the last page
                doc.Form.Add(sigField, doc.Pages.Count);

                // Prepare the PKCS7 signature provider
                PKCS7 pkcs = new PKCS7(certificate, certPassword)
                {
                    Reason = "Document approval",
                    Location = "Head Office",
                    ContactInfo = "security@example.com"
                };

                // Set the digest algorithm to SHA‑512 via TimestampSettings (PKCS7 does not expose a direct DigestAlgorithm property)
                // A dummy timestamp server URL is supplied; replace with a real TSA URL if needed.
                pkcs.TimestampSettings = new TimestampSettings(
                    "https://timestamp.example.com", // TSA server URL (can be empty if not used)
                    null,
                    DigestHashAlgorithm.Sha512);

                // Sign the field
                sigField.Sign(pkcs);

                // Save the signed PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF signed successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}
