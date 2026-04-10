using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class SignPdfWithImage
{
    static void Main()
    {
        // Input PDF, certificate (PFX) and image for the visible signature
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string pfxPath        = "certificate.pfx";
        const string pfxPassword    = "pfxPassword";
        const string imagePath      = "signature.png";

        // Ensure all required files exist
        if (!File.Exists(inputPdfPath) ||
            !File.Exists(pfxPath) ||
            !File.Exists(imagePath))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the document
            SignatureField sigField = new SignatureField(doc, rect)
            {
                PartialName = "Signature1" // field name
            };

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // Load the image that will be used as the visible appearance
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // PKCS1 constructor with image stream sets the appearance
                PKCS1 pkcs1 = new PKCS1(imgStream)
                {
                    Reason      = "I agree to the terms.",
                    Location    = "New York, USA",
                    ContactInfo = "contact@example.com"
                };

                // Load the certificate (PFX) and sign the field
                using (FileStream pfxStream = File.OpenRead(pfxPath))
                {
                    // Sign overload that accepts a Signature object, a PFX stream and its password
                    sigField.Sign(pkcs1, pfxStream, pfxPassword);
                }
            }

            // Save the signed PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}