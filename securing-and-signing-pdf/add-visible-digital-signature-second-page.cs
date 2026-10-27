using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certPath = "cert.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("Document has less than two pages.");
                return;
            }

            // Get dimensions of the second page
            Page page = doc.Pages[2];
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Define signature appearance size and margin (points)
            double sigWidth = 150;   // width of the visible signature
            double sigHeight = 50;   // height of the visible signature
            double margin = 20;      // distance from page edges

            // Calculate rectangle positioned at bottom‑right corner
            double llx = pageWidth - sigWidth - margin; // lower‑left X
            double lly = margin;                         // lower‑left Y
            double urx = pageWidth - margin;             // upper‑right X
            double ury = margin + sigHeight;             // upper‑right Y

            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create a signature field on the second page (the constructor places it on the page)
            SignatureField sigField = new SignatureField(doc, rect);
            doc.Form.Add(sigField);

            // NOTE: In the Aspose.Pdf version used, the visible appearance is automatically
            // derived from the rectangle supplied to the SignatureField constructor.
            // Therefore we omit the SignatureAppearance block that caused compilation errors.

            // Create PKCS#1 signature handler using the certificate file (LocationInfo is not supported in this class)
            PKCS1 pkcs = new PKCS1(certPath, certPassword)
            {
                ContactInfo = "contact@example.com",
                Reason = "Document approval"
            };

            // Apply the digital signature to the field
            sigField.Sign(pkcs);

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
