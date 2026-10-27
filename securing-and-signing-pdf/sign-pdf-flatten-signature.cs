using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_flattened.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "certPassword";

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the visible signature (llx, lly, urx, ury)
            var rect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

            // Create a signature field and add it to the form
            var signatureField = new SignatureField(doc, rect)
            {
                Name = "Signature1"
            };
            doc.Form.Add(signatureField);

            // Load the certificate as a PKCS7 object
            var pkcs7 = new PKCS7(certificatePath, certificatePassword)
            {
                Reason = "Document approved",
                Location = "Company"
            };

            // Apply the digital signature to the field
            signatureField.Sign(pkcs7);

            // Flatten the signature appearance so it cannot be altered visually after signing
            signatureField.Flatten();

            // Save the signed and flattened PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPath}'.");
    }
}
