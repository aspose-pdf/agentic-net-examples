using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, and RSA certificate (PFX) paths
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePath = "certificate_4096.pfx";
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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Define the rectangle where the visible signature will appear (coordinates in points)
                // llx, lly, urx, ury
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

                // Create a signature field on the first page
                SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
                {
                    PartialName = "Signature1",          // Field name
                    Color = Aspose.Pdf.Color.LightGray   // Optional background color for the field
                };

                // Add the signature field to the page annotations collection
                doc.Pages[1].Annotations.Add(sigField);

                // Create a PKCS#1 signature object using the RSA certificate
                // This uses the RSA private key (4096‑bit) contained in the PFX file
                PKCS1 pkcs1Signature = new PKCS1(certificatePath, certificatePassword)
                {
                    Reason      = "Document approval",
                    ContactInfo = "signer@example.com",
                    Location    = "New York, USA",
                    Date        = DateTime.UtcNow
                };

                // Sign the document using the created signature field
                sigField.Sign(pkcs1Signature);

                // Save the signed PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF successfully signed and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}