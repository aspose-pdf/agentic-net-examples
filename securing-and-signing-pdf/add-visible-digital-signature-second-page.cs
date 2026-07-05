using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_output.pdf"; // result PDF
        const string pfxPath    = "certificate.pfx";   // signing certificate
        const string pfxPassword = "pfxPassword";      // certificate password

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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("Document does not contain a second page.");
                return;
            }

            // Define the rectangle for the visible signature field.
            // Bottom‑right corner: adjust width/height as needed.
            // Coordinates are (llx, lly, urx, ury) in points.
            // Here we place a 150x50 rectangle 20 points from the right and bottom edges.
            double pageWidth  = doc.Pages[2].PageInfo.Width;
            double pageHeight = doc.Pages[2].PageInfo.Height;
            double sigWidth   = 150;
            double sigHeight  = 50;
            double llx = pageWidth  - sigWidth - 20; // left
            double lly = 20;                         // bottom
            double urx = pageWidth  - 20;            // right
            double ury = lly + sigHeight;            // top

            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create a signature field on the second page.
            SignatureField sigField = new SignatureField(doc.Pages[2], sigRect)
            {
                // Optional: set a name for the field (useful for later reference)
                Name = "VisibleSignature"
            };
            // Add the field to the page's annotations collection.
            doc.Pages[2].Annotations.Add(sigField);

            // Create a concrete PKCS7 signature object from the PFX file.
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason       = "Document approved",
                Location     = "Office",
                ContactInfo  = "contact@example.com"
            };

            // Sign the document using the created signature field.
            sigField.Sign(pkcs7);

            // Save the signed PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
