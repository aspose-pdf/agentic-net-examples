using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (1‑based page indexing)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the signature will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page
            SignatureField signatureField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };

            // Add the signature field to the document's form collection
            doc.Form.Add(signatureField, 1);

            // Create a PKCS#1 signature object using the RSA 4096‑bit certificate (PFX)
            PKCS1 pkcs1 = new PKCS1(pfxPath, pfxPassword)
            {
                Reason = "Document approved",
                Location = "Company HQ",
                ContactInfo = "contact@example.com",
                Date = DateTime.UtcNow
            };

            // Sign the field with the PKCS#1 signature
            signatureField.Sign(pkcs1);

            // Save the signed PDF (writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPath}'.");
    }
}