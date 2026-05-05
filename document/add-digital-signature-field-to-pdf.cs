using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string pfxPath        = "certificate.pfx";   // self‑signed certificate file
        const string pfxPassword    = "password";          // certificate password

        // Verify that required files exist
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

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document doc = new Document(inputPdfPath))
            {
                // Define the rectangle where the signature field will appear
                // Rectangle(left, bottom, width, height)
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 200, 50);

                // Create a signature field on the first page (page indexing is 1‑based)
                SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);
                // Add the field to the document's form collection
                doc.Form.Add(sigField);

                // Use a concrete PKCS7 signature (Signature is abstract)
                PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
                pkcs7.Reason   = "Document approved";
                pkcs7.Location = "Office";
                pkcs7.Date     = DateTime.Now;

                // Sign the document using the created signature field
                sigField.Sign(pkcs7);

                // Save the signed PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
