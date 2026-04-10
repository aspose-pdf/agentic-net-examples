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
        const string pfxPassword = "password";         // certificate password

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
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Define the rectangle for the visible signature field (bottom‑right corner)
            // Coordinates: left, bottom, right, top (points). Adjust as needed.
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(400, 50, 550, 120);

            // Create the signature field on page 2
            SignatureField sigField = new SignatureField(doc.Pages[2], sigRect);

            // Add the field to the document's form collection
            doc.Form.Add(sigField);

            // Create a concrete PKCS7 signature object using the PFX certificate
            PKCS7 signature = new PKCS7(pfxPath, pfxPassword);

            // Optional: set additional signature properties (reason, location, etc.)
            signature.Reason   = "Approved";
            signature.Location = "Office";

            // Sign the document using the created signature field
            sigField.Sign(signature);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
