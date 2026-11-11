using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string certPath = "cert.pfx";
        const string certPassword = "certPassword";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Create a visible signature field at the desired location.
            // ------------------------------------------------------------
            var signatureRect = new Aspose.Pdf.Rectangle(100, 100, 300, 160);
            var sigField = new SignatureField(doc, signatureRect)
            {
                Name = "Signature1"
            };
            doc.Form.Add(sigField);

            // ------------------------------------------------------------
            // 2. Prepare the PKCS7 signature object and its appearance.
            // ------------------------------------------------------------
            var pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Approved",
                Location = "New York, USA",
                ContactInfo = "contact@example.com",
                CustomAppearance = new SignatureCustomAppearance
                {
                    // Semi‑transparent yellow background (alpha 128 out of 255)
                    BackgroundColor = Aspose.Pdf.Color.FromArgb(128, 255, 255, 0),
                    FontSize = 8,
                    ShowReason = true,
                    ShowLocation = true,
                    ShowContactInfo = true,
                    DigitalSignedLabel = "Signed by"
                }
            };

            // ------------------------------------------------------------
            // 3. Sign the field with the PKCS7 object.
            // ------------------------------------------------------------
            sigField.Sign(pkcs7);

            // ------------------------------------------------------------
            // 4. Save the signed PDF.
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
