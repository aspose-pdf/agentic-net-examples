using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

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
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Define the rectangle where the signature will appear (coordinates are in points)
                Rectangle sigRect = new Rectangle(100, 100, 300, 200);

                // Create a signature field on the first page
                SignatureField signatureField = new SignatureField(doc.Pages[1], sigRect)
                {
                    // Optional: give the field a name (PartialName) – this is the name used later to locate the field
                    PartialName = "Signature1"
                };

                // Add the signature field to the document's AcroForm
                doc.Form.Add(signatureField, 1);

                // Create the PKCS#7 (or PKCS#1) digital signature object using the certificate
                // Aspose.Pdf.Forms.PKCS7 is the correct class for a certificate stored in a PFX file.
                PKCS7 pkcsSignature = new PKCS7(certificatePath, certificatePassword)
                {
                    Reason = "Document certified – form filling allowed",
                    Location = "Office",
                    ContactInfo = "support@example.com"
                };

                // Sign the field. The Sign method writes the digital signature into the field.
                signatureField.Sign(pkcsSignature);

                // Save the signed PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully signed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
