using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_locked.pdf"; // signed PDF
        const string pfxPath    = "certificate.pfx";   // PKCS#12 certificate file
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Define the rectangle where the signature field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            SignatureField sigField = new SignatureField(firstPage, sigRect)
            {
                // Optional visual properties
                Color = Aspose.Pdf.Color.LightGray,
                // Make the field read‑only after signing to prevent further edits
                ReadOnly = true
            };

            // Add the signature field to the page annotations collection
            firstPage.Annotations.Add(sigField);

            // Create a PKCS#1 signature object using the certificate file
            // The constructor (string pfx, string password) loads the certificate
            PKCS1 pkcs1Signature = new PKCS1(pfxPath, pfxPassword)
            {
                Reason   = "Document approved",
                Location = "Company HQ",
                ContactInfo = "contact@example.com",
                Date = DateTime.UtcNow
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs1Signature);

            // Optional: enforce incremental updates only (prevents full rewrite)
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed and locked PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}