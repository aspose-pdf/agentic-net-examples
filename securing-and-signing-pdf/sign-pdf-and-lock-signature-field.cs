using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_locked.pdf";
        const string pfxPath        = "certificate.pfx";
        const string pfxPassword    = "password";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField);

            // Initialize a PKCS#1 signature object using the PFX certificate
            PKCS1 pkcs1 = new PKCS1(pfxPath, pfxPassword)
            {
                Reason      = "Approved",
                Location    = "Head Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the field
            sigField.Sign(pkcs1);

            // Make the signature field read‑only to prevent further edits
            sigField.ReadOnly = true;

            // Enable AppendOnly mode so that only additional signatures can be added later
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed and locked PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed and locked PDF saved to '{outputPdfPath}'.");
    }
}
