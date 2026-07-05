using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output signed PDF, and certificate (PFX) details
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string pfxPath        = "certificate.pfx";
        const string pfxPassword    = "pfxPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page (position: lower‑left X,Y, width, height)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);
            SignatureField signatureField = new SignatureField(doc, sigRect)
            {
                // Optional: give the field a name that can be referenced later
                Name = "Signature1"
            };

            // Prepare a PKCS#7 signature object using the certificate file
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason   = "Document approved",
                Location = "Office",
                ContactInfo = "signer@example.com",
                Date = DateTime.UtcNow
            };

            // Sign the document using the created signature field
            signatureField.Sign(pkcs7);

            // Indicate that the document may receive additional signatures.
            // This flag helps callers understand that incremental updates are required.
            doc.Form.SignaturesAppendOnly = true;

            // Save the document. Document.Save() performs an incremental update,
            // preserving the existing signature and allowing further signatures.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPdfPath}'.");
        Console.WriteLine("The document can now be opened and additional signatures added by other users.");
    }
}