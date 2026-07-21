using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "filled_form.pdf";      // PDF with filled form fields
        const string outputPdfPath = "signed_form.pdf";      // Resulting signed PDF
        const string pfxPath = "certificate.pfx";           // PKCS#12 certificate file
        const string pfxPassword = "pfxPassword";           // Password for the certificate

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (deterministic disposal with using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page
            Page page = doc.Pages[1]; // 1‑based indexing
            var sigRect = new Rectangle(100, 100, 300, 150);
            var sigField = new SignatureField(page, sigRect)
            {
                Name = "DocSignature" // optional field name
            };
            page.Annotations.Add(sigField);

            // Use a concrete PKCS7 signature (Signature is abstract)
            var pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document certification",
                Location = "Company HQ",
                ContactInfo = "contact@example.com",
                Date = DateTime.UtcNow
            };

            // Apply the digital signature to the field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
