using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "zugferd_input.pdf";
        const string outputPdfPath = "zugferd_signed.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "pfxPassword";
        const string timestampUrl = "http://timestamp.server/url";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX certificate not found: {pfxPath}");
            return;
        }

        try
        {
            // Load the ZUGFeRD PDF
            using (Document doc = new Document(inputPdfPath))
            {
                // Define the signature field rectangle (coordinates in points)
                // Adjust the values to position the signature as required
                Rectangle sigRect = new Rectangle(100, 100, 250, 150);

                // Create the signature field on the first page
                SignatureField signatureField = new SignatureField(doc.Pages[1], sigRect);
                signatureField.PartialName = "Signature1";
                signatureField.Name = "Signature1";

                // Add the signature field to the form (page number is 1‑based)
                doc.Form.Add(signatureField, 1);

                // Prepare PKCS#7 signature credentials
                PKCS7 pkcs = new PKCS7(pfxPath, pfxPassword);
                pkcs.Reason = "Document approval";
                pkcs.Location = "Company Headquarters";
                pkcs.ContactInfo = "support@example.com";
                // Configure timestamping (no basic auth in this example)
                pkcs.TimestampSettings = new Aspose.Pdf.TimestampSettings(timestampUrl, null);

                // Sign the field – Signature property is read‑only, so we call Sign()
                signatureField.Sign(pkcs);

                // Save the signed PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}
