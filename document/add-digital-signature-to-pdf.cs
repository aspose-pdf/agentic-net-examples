using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – replace with actual files
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string pfxPath        = "certificate.pfx"; // self‑signed certificate file
        const string pfxPassword    = "password";        // certificate password

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the certificate file exists
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a rectangle that defines the position and size of the signature field
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field on the first page
            // Page indexing in Aspose.Pdf is 1‑based (global rule)
            Page firstPage = pdfDoc.Pages[1];
            SignatureField sigField = new SignatureField(firstPage, sigRect)
            {
                // Optional: set a name for the field (used as tooltip in viewers)
                Name = "Signature1",
                // Optional: set a tooltip (alternate name)
                AlternateName = "Document Signature"
            };

            // Add the signature field to the page's annotation collection
            firstPage.Annotations.Add(sigField);

            // Create a PKCS#7 signature object using the self‑signed certificate
            // The constructor accepts the path to the .pfx file and its password
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword)
            {
                // Optional: set appearance properties
                Reason = "I approve this document",
                Location = "My Office",
                ContactInfo = "email@example.com",
                Date = DateTime.UtcNow
            };

            // Sign the document using the signature field (method defined on SignatureField)
            sigField.Sign(pkcs7Signature);

            // Save the signed PDF (lifecycle rule: use Document.Save within the using block)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}