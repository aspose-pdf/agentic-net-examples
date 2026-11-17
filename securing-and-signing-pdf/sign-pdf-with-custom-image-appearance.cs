using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class SignPdfWithImage
{
    static void Main()
    {
        // Input PDF, output PDF, certificate (PFX) and its password
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePwd = "certPassword";
        const string signatureImagePath = "signature.png";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate not found: {certificatePath}");
            return;
        }
        if (!File.Exists(signatureImagePath))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImagePath}");
            return;
        }

        try
        {
            // Load the source PDF into an expandable stream (required because signing writes back to the same stream)
            using var inputStream = new MemoryStream();
            using (var fileStream = File.OpenRead(inputPdfPath))
                fileStream.CopyTo(inputStream);
            inputStream.Position = 0;

            // Create Aspose.Pdf.Document from the expandable stream
            using var document = new Document(inputStream);

            // Define the rectangle where the visible signature will appear (coordinates are in points)
            var signatureRect = new Rectangle(100, 100, 250, 150);

            // Add a signature field to the first page
            var signatureField = new SignatureField(document.Pages[1], signatureRect)
            {
                PartialName = "VisibleSignature"
            };
            document.Form.Add(signatureField, 1);

            // Load the image that will be used as the visual appearance
            using var imageStream = File.OpenRead(signatureImagePath);

            // Create a PKCS7Detached object – it carries the image for the appearance
            var pkcs7 = new PKCS7Detached(imageStream)
            {
                Reason = "Document approval",
                Location = "New York, USA",
                ContactInfo = "john.doe@example.com"
                // You can further customise the appearance via CustomAppearance if needed
            };

            // Load the certificate (PFX) into a stream – the Sign method expects the certificate stream and password
            using var certStream = File.OpenRead(certificatePath);

            // Sign the field. The overload writes the signed PDF back into the original stream (inputStream).
            signatureField.Sign(pkcs7, certStream, certificatePwd);

            // Write the signed PDF to the output file. No additional Document.Save call is required because the
            // signing operation already updated the underlying stream.
            File.WriteAllBytes(outputPdfPath, inputStream.ToArray());

            Console.WriteLine($"PDF signed successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}
