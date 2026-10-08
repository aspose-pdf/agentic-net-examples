using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // PKCS7, TimestampSettings, DigestHashAlgorithm, SignatureCustomAppearance, SignatureField

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed.pdf";
        const string certificatePath = "mycert.pfx";
        const string certificatePassword = "password";
        const string tsaUrl = "http://timestamp.digicert.com"; // replace with a trusted TSA URL

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle for the signature field (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

            // Create the signature field on the first page and add it to the document's form collection
            SignatureField sigField = new SignatureField(doc, sigRect);
            doc.Form.Add(sigField);

            // Configure visual appearance of the signature using SignatureCustomAppearance
            SignatureCustomAppearance appearance = new SignatureCustomAppearance
            {
                BackgroundColor = Color.LightGray,
                ShowContactInfo = true,
                ShowLocation = true,
                ContactInfoLabel = "Contact:",
                LocationLabel = "Location:"
                // Additional appearance options can be set here (e.g., Image, Text, etc.)
            };

            // Create a PKCS#7 signature object, attach certificate, appearance and timestamp settings
            PKCS7 pkcs = new PKCS7(certificatePath, certificatePassword)
            {
                CustomAppearance = appearance,
                TimestampSettings = new TimestampSettings(
                    tsaUrl,
                    null, // no username/password for this TSA
                    DigestHashAlgorithm.Sha256)
            };

            // Apply the digital signature to the field
            sigField.Sign(pkcs);

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
