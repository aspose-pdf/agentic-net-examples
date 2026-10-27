using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input and output PDF files
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";

        // Certificate (PKCS#12) used for signing
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "certPassword";

        // Signature metadata
        const string reason = "Document approved for release";
        const string location = "Head Office";
        const string contact = "compliance@example.com";

        // Custom XML data to embed in the signature (must conform to external standard)
        const string customXmlSignatureData = @"<SignatureInfo>
    <ComplianceStandard>ISO-12345</ComplianceStandard>
    <SignedBy>John Doe</SignedBy>
    <SignedOn>2024-10-26T12:34:56Z</SignedOn>
</SignatureInfo>";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Create a signature field on the first page (adjust rectangle as needed)
            // Rectangle(left, bottom, right, top)
            var signatureRect = new Rectangle(100, 100, 300, 200);
            SignatureField signatureField = new SignatureField(pdfDocument.Pages[1], signatureRect)
            {
                PartialName = "Signature1"
            };
            // Add the field to the document's form (page number is 1‑based)
            pdfDocument.Form.Add(signatureField, 1);

            // Prepare the PKCS7 signature object
            PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
            {
                Reason = reason,
                Location = location,
                ContactInfo = contact,
                CustomAppearance = new SignatureCustomAppearance
                {
                    BackgroundColor = Aspose.Pdf.Color.LightGray,
                    IsForegroundImage = false // no image, just background color
                }
            };

            // Sign the field
            signatureField.Sign(pkcs7);

            // Embed the custom XML as an embedded file (compliance data)
            using (var xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(customXmlSignatureData)))
            {
                var xmlFileSpec = new FileSpecification("SignatureInfo.xml", "Custom XML signature data");
                xmlFileSpec.Contents = xmlStream;
                // Optionally set modification date
                xmlFileSpec.Params.ModDate = DateTime.UtcNow;
                pdfDocument.EmbeddedFiles.Add(xmlFileSpec);
            }

            // Save the signed PDF
            pdfDocument.Save(outputPdfPath);

            Console.WriteLine($"PDF signed successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (System.Security.Cryptography.CryptographicException ex)
        {
            Console.Error.WriteLine($"Certificate error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Signing failed: {ex.Message}");
        }
    }
}
