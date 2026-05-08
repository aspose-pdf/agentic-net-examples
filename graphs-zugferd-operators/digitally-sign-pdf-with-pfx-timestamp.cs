using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPdf) || !File.Exists(pfxPath))
        {
            Console.Error.WriteLine("Missing input PDF or PFX certificate.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the signature will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a signature field on the page
            SignatureField signatureField = new SignatureField(page, rect)
            {
                PartialName = "Signature1"
            };

            // Add the signature field to the page annotations
            page.Annotations.Add(signatureField);

            // Load the PFX certificate as a stream
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                // Create a PKCS#7 signature object using the certificate
                PKCS7 pkcs7 = new PKCS7(pfxStream, pfxPassword)
                {
                    Reason = "Document approved",
                    Location = "Office",
                    ContactInfo = "contact@example.com",
                    ShowProperties = true // display signature properties in appearance
                };

                // Configure timestamp settings (RFC 3161 server)
                pkcs7.TimestampSettings = new TimestampSettings(
                    "http://timestamp.digicert.com",
                    "", // basic authentication credentials (if any)
                    DigestHashAlgorithm.Sha256);

                // Sign the document using the signature field
                signatureField.Sign(pkcs7);
            }

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
