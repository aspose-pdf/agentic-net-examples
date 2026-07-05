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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Define the rectangle where the signature field will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the specified page and rectangle
            SignatureField sigField = new SignatureField(page, rect);
            doc.Form.Add(sigField); // Add the field to the document's form

            // Create a PKCS#7 signature object using a PFX certificate
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7);

            // Lock the signature field to prevent further modifications
            sigField.ReadOnly = true;

            // Optional: ensure that existing signatures cannot be invalidated by non‑incremental saves
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}