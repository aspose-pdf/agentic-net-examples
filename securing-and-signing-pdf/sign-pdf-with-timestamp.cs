using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_timestamped.pdf";
        const string certificate = "certificate.pfx";
        const string certPassword = "certPassword";
        const string tsaUrl = "http://timestamp.digicert.com";

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
            // Load the PDF document
            Document doc = new Document(inputPdf);

            // Define the rectangle where the signature will appear (coordinates are in points)
            // Here we place it on the first page at (100,100) with width 200 and height 100
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field and add it to the document's form collection
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                Name = "Signature1"
            };
            doc.Form.Add(sigField);

            // Prepare the PKCS#7 signature using the provided certificate
            PKCS7 pkcs7 = new PKCS7(certificate, certPassword)
            {
                Reason = "Document signed",
                Location = "MyCompany"
                // SigningTime is automatically set; no explicit property exists.
            };

            // Apply a timestamp from a trusted TSA server using TimestampSettings
            pkcs7.TimestampSettings = new TimestampSettings(tsaUrl, string.Empty);

            // Sign the field with the prepared PKCS#7 object
            sigField.Sign(pkcs7);

            // Save the signed and timestamped PDF
            doc.Save(outputPdf);

            Console.WriteLine($"Signed PDF with timestamp saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
