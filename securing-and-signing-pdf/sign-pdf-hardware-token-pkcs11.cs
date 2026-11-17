using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Drawing; // for Rectangle used by the signature field

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        // NOTE: The example below uses a PFX file for demonstration. 
        // When signing with a hardware token you would obtain the certificate
        // and private key via the token's PKCS#11 library and create a PKCS7
        // instance accordingly (the API supports PKCS#11 through PKCS11SignatureOptions
        // in newer versions, but that class resides in a different namespace that
        // is not available in the current package version). The core signing flow
        // remains the same – create a SignatureField, configure a PKCS7 object and
        // call field.Sign(pkcs7).
        const string certificatePath = "certificate.pfx"; // replace with your token‑derived cert if needed
        const string certificatePassword = "password";   // replace with the token PIN or cert password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
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
            using (Document pdfDoc = new Document(inputPath))
            {
                // Define a visible signature field on the first page
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                SignatureField sigField = new SignatureField(pdfDoc.Pages[1], rect)
                {
                    PartialName = "Signature1"
                };
                pdfDoc.Form.Add(sigField, 1);

                // Configure the PKCS7 signature (replace with PKCS#11 handling as needed)
                PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
                {
                    Reason = "Document signed with hardware token",
                    Location = "Company",
                    ContactInfo = "support@company.com"
                };

                // Apply the digital signature through the field
                sigField.Sign(pkcs7);

                // Save the signed PDF
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"PDF signed successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error signing PDF: {ex.Message}");
        }
    }
}
