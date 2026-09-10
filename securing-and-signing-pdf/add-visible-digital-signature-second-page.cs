using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("Document does not contain a second page.");
                return;
            }

            // Get the second page (1‑based indexing)
            Page page = doc.Pages[2];

            // Define signature field size
            const double sigWidth  = 150; // points
            const double sigHeight = 50;  // points
            const double margin    = 20;  // points from edges

            // Calculate bottom‑right rectangle coordinates
            double llx = page.Rect.URX - sigWidth - margin; // lower‑left X
            double lly = page.Rect.LLY + margin;            // lower‑left Y
            double urx = llx + sigWidth;                    // upper‑right X
            double ury = lly + sigHeight;                   // upper‑right Y

            // Create the rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create a visible signature field on the second page
            SignatureField sigField = new SignatureField(page, rect)
            {
                // Optional: give the field a name (useful for later reference)
                Name = "VisibleSignature"
            };

            // Add the signature field to the page annotations collection
            page.Annotations.Add(sigField);

            // Prepare the digital signature (PKCS#1 or PKCS#7)
            // Here we use PKCS#1; replace with PKCS7 if required
            Signature signature = new PKCS1(certPath, certPass)
            {
                Reason      = "Document approved",
                ContactInfo = "contact@example.com",
                Location    = "New York"
            };

            // Sign the field
            sigField.Sign(signature);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}