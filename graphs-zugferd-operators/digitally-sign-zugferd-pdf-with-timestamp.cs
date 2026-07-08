using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "zugferd_input.pdf";
        const string outputPdfPath = "zugferd_signed.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "pfxPassword";
        const string timestampUrl = "http://timestamp.example.com";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (ZUGFeRD PDF)
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page
            // Rectangle(left, bottom, width, height)
            var rect = new Rectangle(100, 100, 200, 150);
            var sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };

            // Add the signature field to the page annotations
            doc.Pages[1].Annotations.Add(sigField);

            // Create a PKCS#7 signature object using the PFX file
            var pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Approved",
                Location = "Company HQ",
                ContactInfo = "signer@example.com",
                // Configure timestamp settings (serverUrl, username, hash algorithm)
                TimestampSettings = new TimestampSettings(timestampUrl, "", DigestHashAlgorithm.Sha256)
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
