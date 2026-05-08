using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "invoice_zugferd.pdf";
        const string outputPdfPath  = "invoice_zugferd_signed.pdf";
        const string pfxPath        = "certificate.pfx";
        const string pfxPassword    = "pfxPassword";
        const string timestampUrl   = "http://timestamp.digicert.com";

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
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Create a signature field on the first page
            Page firstPage = pdfDoc.Pages[1];
            // Define the rectangle where the signature appearance will be placed
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
            SignatureField sigField = new SignatureField(firstPage, sigRect);
            firstPage.Annotations.Add(sigField);

            // Load the PFX certificate into a stream
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                // Create a PKCS#7 signature object using the certificate stream
                PKCS7 pkcs7Signature = new PKCS7(pfxStream, pfxPassword)
                {
                    Reason      = "Document approved",
                    Location    = "Company HQ",
                    ContactInfo = "contact@company.com",
                    // Include a timestamp (using the correct constructor signature)
                    TimestampSettings = new TimestampSettings(timestampUrl, "", DigestHashAlgorithm.Sha256)
                };

                // Sign the PDF using the signature field
                sigField.Sign(pkcs7Signature);
            }

            // Save the signed PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
