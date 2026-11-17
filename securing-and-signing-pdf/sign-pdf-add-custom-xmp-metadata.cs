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
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "certPassword";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Embed custom XMP metadata describing the signing process
                doc.Metadata["SigningProcess"] = "Signed using Aspose.Pdf for .NET with a digital certificate.";

                // Create a signature field on page 1 (pages are 1‑based in the API)
                // Rectangle coordinates: lower‑left (100,100), upper‑right (200,150)
                var signatureRect = new Rectangle(100, 100, 200, 150);
                var signatureField = new SignatureField(doc.Pages[1], signatureRect)
                {
                    PartialName = "Signature1"
                };
                // Add the field to the document's form collection
                doc.Form.Add(signatureField, 1);

                // Prepare the PKCS#7 signature using the provided certificate
                var pkcs = new PKCS7(certificatePath, certificatePassword)
                {
                    Reason = "Document signed",
                    Location = "MyCompany",
                    ContactInfo = "contact@mycompany.com"
                };

                // Apply the digital signature to the field
                signatureField.Sign(pkcs);

                // Save the signed PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
