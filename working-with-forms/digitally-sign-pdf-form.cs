using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "filled_form.pdf";      // PDF with filled form fields
        const string outputPdfPath  = "signed_form.pdf";      // Output signed PDF
        const string pfxPath        = "certificate.pfx";     // PKCS#12 certificate file
        const string pfxPassword    = "pfxPassword";         // Password for the certificate

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

        // Load the PDF document (using the lifecycle rule for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Define the rectangle where the signature field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                // Optional: set a tooltip (alternate name) for the field
                AlternateName = "Document Signature"
            };

            // Add the signature field to the page's annotation collection
            doc.Pages[1].Annotations.Add(sigField);

            // Create a concrete PKCS7 signature object using the PFX file and password
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                // Optional metadata for the signature
                Reason      = "Certified copy of the filled form",
                Location    = "Company Headquarters",
                ContactInfo = "support@example.com",
                Date        = DateTime.UtcNow
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7);

            // Save the signed PDF (using the standard Save method)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
