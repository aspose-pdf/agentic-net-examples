using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, and signing certificate (PFX) paths
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string pfxPath    = "certificate.pfx";
        const string pfxPassword = "pfxPassword";

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Get the second page (Aspose.Pdf uses 1‑based indexing)
            Page secondPage = doc.Pages[2];

            // Define the size of the visible signature field (e.g., 150x50 points)
            const double fieldWidth  = 150;
            const double fieldHeight = 50;

            // Position the field at the bottom‑right corner of the page
            // Bottom‑right coordinates: (URX - fieldWidth, LLY) to (URX, LLY + fieldHeight)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(
                secondPage.Rect.URX - fieldWidth,   // lower‑left X
                secondPage.Rect.LLY,                // lower‑left Y
                secondPage.Rect.URX,                // upper‑right X
                secondPage.Rect.LLY + fieldHeight   // upper‑right Y
            );

            // Create a signature field on the document
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                // Optional: set a name for the field (useful for later reference)
                Name = "VisibleSignature"
            };

            // Add the signature field to the document's form
            doc.Form.Add(sigField);

            // Create a concrete PKCS7 signature object (Signature is abstract)
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason      = "Document approved",
                Location    = "Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the document using the created signature field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
