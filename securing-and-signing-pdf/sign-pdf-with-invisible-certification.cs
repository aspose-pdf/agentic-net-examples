using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF
        const string outputPath = "signed.pdf";        // signed PDF
        const string certificatePath = "certificate.pfx"; // PKCS#12 certificate
        const string certificatePassword = "password";   // certificate password

        // Verify required files exist
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
        Document doc = new Document(inputPath);

        // Create PKCS7 credentials (certificate + password)
        PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
        {
            Reason = "Document certified – no changes allowed",
            Location = "Location",
            ContactInfo = "contact@example.com"
        };

        // Add an (invisible) signature field to the first page
        // Rectangle with zero size makes the field invisible
        SignatureField sigField = new SignatureField(doc.Pages[1], new Aspose.Pdf.Rectangle(0, 0, 0, 0))
        {
            PartialName = "Certification"
        };
        doc.Form.Add(sigField, 1);

        // Apply the digital signature (certification is not directly supported via core API)
        sigField.Sign(pkcs7);

        // Save the signed PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF signed and saved to '{outputPath}'.");
    }
}
