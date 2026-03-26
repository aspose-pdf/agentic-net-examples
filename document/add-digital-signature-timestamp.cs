using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";
        const string tsaUrl = "http://timestamp.digicert.com";
        const string tsaCredentials = ""; // username:password if required, otherwise empty

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine("Certificate file not found: " + pfxPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a signature field on the first page
            // Use the fully‑qualified Aspose.Pdf.Rectangle for the annotation bounds
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc.Pages[1], rect);
            sigField.PartialName = "Signature1";
            doc.Form.Add(sigField);

            // Create a PKCS7 signature object from the PFX certificate
            using (FileStream certStream = File.OpenRead(pfxPath))
            {
                PKCS7 pkcs7 = new PKCS7(certStream, pfxPassword);
                pkcs7.Reason = "Document approved";
                pkcs7.Location = "Company HQ";
                pkcs7.ContactInfo = "contact@example.com";

                // Configure timestamp settings (TSA)
                TimestampSettings tsSettings = new TimestampSettings(tsaUrl, tsaCredentials);
                pkcs7.TimestampSettings = tsSettings;

                // Sign the field
                sigField.Sign(pkcs7);
            }

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("Signed PDF saved to " + outputPath);
    }
}
