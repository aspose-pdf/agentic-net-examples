using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

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

        using (Document document = new Document(inputPath))
        {
            // Create a signature field on page 1
            Page page = document.Pages[1];
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
            SignatureField signatureField = new SignatureField(page, fieldRect)
            {
                PartialName = "Signature1"
            };
            // Add the field to the form collection – use Document.Form.Add(field, pageNumber)
            document.Form.Add(signatureField, 1);

            // Sign the document using the created signature field
            PdfFileSignature pdfSigner = new PdfFileSignature();
            pdfSigner.BindPdf(document);
            pdfSigner.SetCertificate(certificatePath, certificatePassword);
            PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword)
            {
                Reason = "I agree",
                ContactInfo = "email@example.com",
                Location = "Office"
            };
            pdfSigner.Sign("Signature1", pkcs7Signature);
            pdfSigner.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
