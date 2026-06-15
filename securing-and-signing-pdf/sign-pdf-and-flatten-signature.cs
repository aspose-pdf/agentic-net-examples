using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, certificate (PFX) and output PDF paths
        const string inputPdfPath   = "input.pdf";
        const string pfxPath        = "certificate.pfx";
        const string pfxPassword    = "pfxPassword";
        const string outputPdfPath  = "signed_flattened.pdf";

        // Ensure input files exist
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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will appear
            // (left, bottom, width, height) – using fully qualified type to avoid ambiguity
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 200, 50);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            // Add the field to the document's form collection
            doc.Form.Add(sigField);

            // Create a PKCS#7 signature object using the PFX file
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword)
            {
                Reason   = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com",
                Date = DateTime.UtcNow
            };

            // Sign the document using the signature field (method from SignatureField)
            sigField.Sign(pkcs7Signature);

            // Flatten the entire document so the signature appearance becomes a static part
            // (prevents further visual modifications)
            doc.Flatten();

            // Save the signed and flattened PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPdfPath}'.");
    }
}