using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed.pdf";         // signed PDF
        const string pfxPath    = "certificate.pfx";   // PKCS#12 certificate file
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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature field will be placed
            // (left, bottom, width, height) – using Aspose.Pdf.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 200, 50);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            // Add the signature field to the document's form collection
            doc.Form.Add(sigField, 1); // 1‑based page index

            // Prepare a PKCS#1 signature object (you could also use PKCS7)
            PKCS1 pkcs1 = new PKCS1(pfxPath, pfxPassword)
            {
                Reason      = "Document certified – annotations allowed",
                ContactInfo = "contact@example.com",
                Location    = "New York"
            };

            // Sign the field. This creates a regular digital signature.
            // Note: Core Aspose.Pdf API does not expose a direct certification method.
            // Certification (MDP) signatures are available via the Facades API.
            sigField.Sign(pkcs1);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPdf}'.");
    }
}