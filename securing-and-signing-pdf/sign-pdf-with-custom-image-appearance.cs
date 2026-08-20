using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, certificate (PFX) and image for the visible signature
        const string pdfPath      = "input.pdf";
        const string pfxPath      = "certificate.pfx";
        const string pfxPassword  = "yourPfxPassword";
        const string imagePath    = "signature.png";
        const string outputPath   = "signed_output.pdf";

        // Verify required files exist
        if (!File.Exists(pdfPath) || !File.Exists(pfxPath) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Define the rectangle where the signature field will be placed (coordinates in points)
            // Adjust values as needed for your layout
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, rect);
            doc.Pages[1].Annotations.Add(sigField);

            // Load the image that will be used as the visible appearance
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // PKCS1 constructor with image stream defines the custom appearance
                PKCS1 pkcs1Signature = new PKCS1(imgStream);

                // Optional: set additional signature properties
                pkcs1Signature.Reason   = "I agree to the terms.";
                pkcs1Signature.Location = "New York, USA";
                pkcs1Signature.ContactInfo = "contact@example.com";

                // Load the certificate (PFX) as a stream
                using (FileStream pfxStream = File.OpenRead(pfxPath))
                {
                    // Sign the field using the PKCS1 signature (appearance) and the certificate stream
                    sigField.Sign(pkcs1Signature, pfxStream, pfxPassword);
                }
            }

            // Save the signed PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF signed successfully. Output saved to '{outputPath}'.");
    }
}