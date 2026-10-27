using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";
        const string signatureImagePath = "signature.png";

        // Validate required files
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }
        if (!File.Exists(signatureImagePath))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImagePath}");
            return;
        }

        // Load the PDF
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the visible signature will appear
            // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Rectangle signatureRect = new Rectangle(100, 100, 250, 150);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, signatureRect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField, 1);

            // Prepare PKCS#7 signature object
            PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
            {
                Reason = "Document approval",
                ContactInfo = "John Doe",
                Location = "New York"
            };

            // Configure custom visual appearance (foreground image)
            pkcs7.CustomAppearance = new SignatureCustomAppearance
            {
                IsForegroundImage = true
                // Additional appearance settings can be added here if needed
            };

            // Sign the field
            sigField.Sign(pkcs7);

            // Add the image stamp so the signature appears visually
            ImageStamp imgStamp = new ImageStamp(signatureImagePath)
            {
                BottomMargin = signatureRect.LLY,
                LeftMargin   = signatureRect.LLX,
                Width        = signatureRect.URX - signatureRect.LLX,
                Height       = signatureRect.URY - signatureRect.LLY
            };
            doc.Pages[1].AddStamp(imgStamp);

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
