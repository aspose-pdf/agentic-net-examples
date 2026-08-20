using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string pfxPath        = "certificate.pfx";
        const string pfxPassword    = "pfxPassword";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page
            // Fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            SignatureField signatureField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1",          // field name
                Color       = Aspose.Pdf.Color.LightGray,
                Contents    = "Signed by Aspose.Pdf"
            };
            doc.Pages[1].Annotations.Add(signatureField);

            // Initialize PKCS#7 signature using the PFX file
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword)
            {
                Reason      = "I agree to the terms.",
                Location    = "New York, USA",
                ContactInfo = "contact@example.com",
                Date        = DateTime.Now,
                ShowProperties = true               // embed default appearance with properties
            };

            // Configure timestamp settings (RFC 3161 timestamp server)
            // TimestampSettings constructor requires serverUrl, optional username, and hash algorithm (PascalCase enum)
            TimestampSettings tsSettings = new TimestampSettings(
                "http://timestamp.digicert.com", // server URL
                "",                               // username (empty if not required)
                DigestHashAlgorithm.Sha256        // hash algorithm – PascalCase
            );
            pkcs7Signature.TimestampSettings = tsSettings;

            // Sign the document using the signature field
            signatureField.Sign(pkcs7Signature);

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
