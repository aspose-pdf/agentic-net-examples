using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and signing certificate
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

        // Time‑Stamp Authority (TSA) details
        const string tsaUrl      = "https://tsa.example.com";          // replace with a real TSA URL
        const string tsaCreds    = "tsaUser:tsaPassword";              // "username:password"

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

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                PartialName = "Signature1"   // optional field name
            };

            // Create a PKCS#7 signature object using the certificate
            PKCS7 pkcs7 = new PKCS7(certPath, certPass)
            {
                Reason      = "Approved for release",
                ContactInfo = "contact@example.com",
                Location    = "New York"
            };

            // Attach timestamp settings (TSA) to the signature
            pkcs7.TimestampSettings = new TimestampSettings(tsaUrl, tsaCreds);

            // Sign the document using the signature field
            sigField.Sign(pkcs7);

            // Add the signature field to the page annotations collection
            doc.Pages[1].Annotations.Add(sigField);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document signed and saved to '{outputPdf}'.");
    }
}