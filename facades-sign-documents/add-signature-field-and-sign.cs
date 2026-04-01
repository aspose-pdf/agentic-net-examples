using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string tempPath = "temp_with_field.pdf";
        const string outputPath = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";
        const string signatureImage = "signature.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine("Certificate file not found: " + certPath);
            return;
        }

        // Load PDF and add a signature field on page 1
        using (Document doc = new Document(inputPath))
        {
            // Create a signature field (page index is 1‑based)
            var rect = new Rectangle(100, 100, 250, 150); // llx, lly, urx, ury
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            // Add the field to the form on page 1
            doc.Form.Add(sigField, 1);

            // Save intermediate PDF that now contains the signature field
            doc.Save(tempPath);
        }

        // Sign the PDF using the previously added signature field
        using (PdfFileSignature pdfSigner = new PdfFileSignature())
        {
            pdfSigner.BindPdf(tempPath);
            pdfSigner.SignatureAppearance = signatureImage;

            PKCS7 pkcs7Signature = new PKCS7(certPath, certPassword)
            {
                Reason = "Document approved",
                ContactInfo = "john.doe@example.com",
                Location = "New York"
            };

            // Sign the field named "Signature1"
            pdfSigner.Sign("Signature1", pkcs7Signature);
            pdfSigner.Save(outputPath);
        }

        // Delete the temporary file
        try
        {
            File.Delete(tempPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Could not delete temporary file: " + ex.Message);
        }

        Console.WriteLine("Signed PDF saved to '" + outputPath + "'.");
    }
}
