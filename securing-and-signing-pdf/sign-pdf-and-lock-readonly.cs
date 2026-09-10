using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Signatures; // for PKCS1 signature class

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_locked.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page
            Page page = doc.Pages[1];
            // Define the rectangle where the signature will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);
            SignatureField sigField = new SignatureField(page, rect)
            {
                PartialName = "Signature1" // field name
            };
            // Add the signature field to the page annotations
            page.Annotations.Add(sigField);

            // Prepare the digital signature (PKCS#1)
            PKCS1 pkcs1Signature = new PKCS1(certPath, certPass)
            {
                Reason      = "Document approved",
                ContactInfo = "john.doe@example.com",
                Location    = "New York"
            };

            // Sign the field
            sigField.Sign(pkcs1Signature);

            // Enforce read‑only mode after signing
            // This makes the document contain append‑only signatures,
            // preventing further modifications without invalidating the signature.
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed and locked PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and locked PDF saved to '{outputPdf}'.");
    }
}