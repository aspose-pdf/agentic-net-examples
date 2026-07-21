using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string pfxPath       = "certificate.pfx";
        const string pfxPassword   = "pfxPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX file not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);
            SignatureField signatureField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            // Add the signature field to the page annotations
            doc.Pages[1].Annotations.Add(signatureField);

            // Prepare the PKCS#7 signature using the PFX certificate
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                PKCS7 pkcs7Signature = new PKCS7(pfxStream, pfxPassword)
                {
                    Reason      = "Document approved",
                    Location    = "Office",
                    ContactInfo = "contact@example.com",
                    Date        = DateTime.Now,
                    ShowProperties = true // display default signature appearance
                };

                // Configure timestamp settings (replace with a real TSA URL if needed)
                pkcs7Signature.TimestampSettings = new TimestampSettings(
                    "http://timestamp.example.com", // serverUrl
                    string.Empty,                    // basicAuthCredentials (none)
                    DigestHashAlgorithm.Sha256      // digest algorithm
                );

                // Sign the document using the signature field
                signatureField.Sign(pkcs7Signature);
            }

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
