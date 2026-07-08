using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";            // source PDF
        const string outputPdf     = "signed_flattened.pdf"; // result PDF
        const string pfxPath       = "certificate.pfx";      // signing certificate
        const string pfxPassword   = "password";             // certificate password

        // Verify required files exist
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
            // Define the rectangle where the signature field will be placed
            // Coordinates are in points (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page and add it to the page annotations
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);
            doc.Pages[1].Annotations.Add(sigField);

            // Create a concrete PKCS#7 signature object from the PFX file (Signature is abstract)
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason      = "Approved",
                Location    = "Head Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the PDF using the created signature field
            sigField.Sign(pkcs7);

            // Flatten the document to make the visual appearance static (no further changes allowed)
            doc.Flatten();

            // Save the signed and flattened PDF (save rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPdf}'.");
    }
}
