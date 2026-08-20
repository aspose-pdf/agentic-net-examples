using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "invoice.pdf";
        const string outputPdf = "invoice_signed.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";
        const string timestampUrl = "http://timestamp.digicert.com";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX file not found: {pfxPath}");
            return;
        }

        // Load the ZUGFeRD PDF
        using (Document doc = new Document(inputPdf))
        {
            // Define the signature appearance rectangle on page 1
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field and add it to the document's form
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField);

            // Load the PFX certificate and configure PKCS#7 signature
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                PKCS7 pkcs7 = new PKCS7(pfxStream, pfxPassword)
                {
                    Reason = "Approved",
                    Location = "Company HQ",
                    ContactInfo = "contact@example.com",
                    // TimestampSettings constructor requires three arguments:
                    // server URL, basic auth credentials (empty if not needed), and digest algorithm.
                    TimestampSettings = new TimestampSettings(timestampUrl, string.Empty, DigestHashAlgorithm.Sha256)
                };

                // Sign the PDF using the signature field
                sigField.Sign(pkcs7);
            }

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
