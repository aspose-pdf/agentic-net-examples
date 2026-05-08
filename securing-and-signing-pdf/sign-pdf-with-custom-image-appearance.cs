using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to required files
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";
        const string signatureImagePath = "signature.png";

        // Verify that all files exist before proceeding
        if (!File.Exists(inputPdfPath) ||
            !File.Exists(certificatePath) ||
            !File.Exists(signatureImagePath))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Define the rectangle where the visible signature will appear
            // (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle signatureRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField signatureField = new SignatureField(pdfDocument.Pages[1], signatureRect);
            pdfDocument.Pages[1].Annotations.Add(signatureField);

            // Load the image that will be used as the signature appearance
            using (FileStream imageStream = File.OpenRead(signatureImagePath))
            {
                // PKCS1 constructor with an image stream defines the custom appearance
                PKCS1 pkcs1Signature = new PKCS1(imageStream)
                {
                    Reason      = "Document approved",
                    Location    = "New York, USA",
                    ContactInfo = "contact@example.com"
                };

                // Load the certificate (PFX) stream
                using (FileStream certStream = File.OpenRead(certificatePath))
                {
                    // Sign the field using the PKCS1 signature and the certificate
                    signatureField.Sign(pkcs1Signature, certStream, certificatePassword);
                }
            }

            // Save the signed PDF
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}