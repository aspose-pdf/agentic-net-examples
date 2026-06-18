using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_locked.pdf"; // result PDF
        const string certPath   = "certificate.pfx";   // signing certificate
        const string certPass   = "password";           // certificate password

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Create a PKCS#1 signature object and set its properties
            // -----------------------------------------------------------------
            PKCS1 signature = new PKCS1(certPath, certPass);
            signature.Reason      = "Document approved";
            signature.ContactInfo = "support@example.com";
            signature.Location    = "New York, USA";

            // -----------------------------------------------------------------
            // 2. Define a visible signature field on the first page
            // -----------------------------------------------------------------
            // Aspose.Pdf.Rectangle is fully qualified to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);
            // Create the signature field and associate it with the page
            SignatureField sigField = new SignatureField(doc.Pages[1], rect);
            // Assign a name to the field
            sigField.PartialName = "Signature1";
            // Add the field to the page's annotation collection (this also registers it with the form)
            doc.Pages[1].Annotations.Add(sigField);

            // -----------------------------------------------------------------
            // 3. Sign the field using the prepared signature object
            // -----------------------------------------------------------------
            sigField.Sign(signature);

            // -----------------------------------------------------------------
            // 4. (Optional) Lock the document for further modifications.
            //    Aspose.Pdf core API does not expose a direct "read‑only" flag.
            //    The signature itself prevents further changes without invalidating it.
            //    If a stricter lock is required, it must be applied via the Facade API
            //    (PdfFileSignature) which is outside the allowed namespace scope.
            // -----------------------------------------------------------------

            // -----------------------------------------------------------------
            // 5. Save the signed PDF
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
