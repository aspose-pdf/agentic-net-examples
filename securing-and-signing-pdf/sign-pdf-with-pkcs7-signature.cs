using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        // Verify required files exist
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

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will appear
            // (llx, lly, urx, ury) – all values are in points
            Aspose.Pdf.Rectangle signatureRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField signatureField = new SignatureField(pdfDocument.Pages[1], signatureRect)
            {
                Name = "Signature1"
                // Optional: set a visual appearance image
                // SignatureField.CustomAppearance = "appearance.png";
            };

            // Add the signature field to the page annotations collection
            pdfDocument.Pages[1].Annotations.Add(signatureField);

            // Create a PKCS#7 signature object using the certificate
            PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword)
            {
                Reason      = "Document approved",
                ContactInfo = "john.doe@example.com",
                Location    = "New York"
                // Custom XML data can be embedded via the CustomSignHash delegate if required
                // CustomSignHash = (hash) => { /* custom signing logic */ return hash; };
            };

            // Sign the field with the prepared signature object
            signatureField.Sign(pkcs7Signature);

            // Save the signed PDF
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}