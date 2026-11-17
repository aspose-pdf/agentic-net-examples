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
        const string certificatePassword = "certPassword";

        // Verify required files exist
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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Define the rectangle where the visible signature will be placed
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

                // Create a signature field on the first page
                Page page = doc.Pages[1];
                SignatureField sigField = new SignatureField(page, sigRect)
                {
                    PartialName = "Signature1"
                };

                // Add the signature field to the document's form collection
                doc.Form.Add(sigField, page.Number);

                // Prepare PKCS7 signature object with reason, contact info and location
                PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
                {
                    Reason = "Document approved",
                    ContactInfo = "john.doe@example.com",
                    Location = "New York, USA"
                };

                // Sign the field
                sigField.Sign(pkcs7);

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
