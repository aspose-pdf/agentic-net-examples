using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "signed_locked.pdf"; // signed PDF
        const string pfxPath   = "certificate.pfx";   // signing certificate
        const string pfxPass   = "pfxPassword";       // certificate password

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                // Optional: set a tooltip (alternate name) for the field
                AlternateName = "Document Signature"
            };

            // Add the signature field to the page annotations collection
            doc.Pages[1].Annotations.Add(sigField);

            // Create a PKCS#1 signature object using the PFX file and password
            PKCS1 pkcs1Signature = new PKCS1(pfxPath, pfxPass)
            {
                Reason   = "Approved",
                Location = "Office",
                ContactInfo = "contact@example.com",
                Date = DateTime.UtcNow
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs1Signature);

            // Lock the signature field to prevent further modifications
            sigField.ReadOnly = true;

            // Save the signed and locked PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}