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
        const string outputPath = "signed_locked.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine("Certificate file not found: " + certificatePath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a visible signature field on the first page
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField signatureField = new SignatureField(doc.Pages[1], sigRect);
            signatureField.Name = "Signature1";
            signatureField.PartialName = "Signature1";
            signatureField.Color = Aspose.Pdf.Color.LightGray;
            doc.Form.Add(signatureField);

            // Sign the field using a PKCS#7 certificate
            PdfFileSignature pdfSignature = new PdfFileSignature(doc);
            PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword);
            pkcs7.Reason = "Document approved";
            pkcs7.Location = "Office";
            pkcs7.ContactInfo = "contact@example.com";
            pdfSignature.Sign(signatureField.Name, pkcs7);

            // Lock the document (allow only printing)
            doc.Encrypt(string.Empty, "ownerPassword", Permissions.PrintDocument, CryptoAlgorithm.AESx256);

            // Save the signed and locked PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("Signed and locked PDF saved to '" + outputPath + "'.");
    }
}