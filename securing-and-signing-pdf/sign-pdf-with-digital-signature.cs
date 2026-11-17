using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath      = "input.pdf";
        const string outputPdfPath     = "signed_output.pdf";
        const string certificatePath   = "certificate.pfx";
        const string certificatePwd    = "pfxPassword";

        // Verify files exist
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
            // Load the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Create a signature field on the first page
                // Define the rectangle where the visual appearance will be placed
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
                SignatureField signatureField = new SignatureField(doc.Pages[1], sigRect);
                signatureField.PartialName = "Signature1";

                // Add the signature field to the document's form collection
                doc.Form.Add(signatureField, 1);

                // Load the PKCS#7 certificate (PFX)
                PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePwd);

                // Sign the field
                // The Sign method creates the digital signature using the provided PKCS7 object
                signatureField.Sign(pkcs7);

                // Save the signed PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}