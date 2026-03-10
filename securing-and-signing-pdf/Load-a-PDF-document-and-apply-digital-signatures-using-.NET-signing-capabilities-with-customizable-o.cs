using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, and certificate (PFX) paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "pfxPassword";

        // Verify that required files exist
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the visible signature will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle signatureRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page and add it to the form collection
            // The Form class does not expose AddSignatureField in some versions; use the constructor and Form.Add()
            SignatureField signatureField = new SignatureField(doc.Pages[1], signatureRect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(signatureField);

            // Load the certificate (PFX) into a stream
            using (FileStream pfxStream = File.OpenRead(certificatePath))
            {
                // Create a PKCS#7 signature object using the certificate stream and password
                PKCS7 pkcs7Signature = new PKCS7(pfxStream, certificatePassword)
                {
                    // Optional appearance and metadata properties
                    Reason = "Document approved",
                    Location = "New York, USA",
                    ContactInfo = "contact@example.com",
                    Date = DateTime.UtcNow,
                    ShowProperties = true // display signature properties in the appearance
                };

                // Sign the document using the signature field
                signatureField.Sign(pkcs7Signature);
            }

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
