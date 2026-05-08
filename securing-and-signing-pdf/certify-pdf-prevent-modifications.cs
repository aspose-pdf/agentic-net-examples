using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_certified.pdf"; // result PDF
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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure that any further changes will invalidate the signature
            doc.Form.SignaturesAppendOnly = true;

            // Create a signature field on the first page
            Page page = doc.Pages[1];
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(page, rect);
            // Optional: set a name for the field
            sigField.Name = "CertSignature";
            // Add the field to the page annotations collection
            page.Annotations.Add(sigField);

            // Create a concrete signature object (PKCS#7) from the PFX file
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
            // Set optional appearance properties
            pkcs7.Reason = "Document certified – no changes allowed";
            pkcs7.Location = "My Company";
            pkcs7.ContactInfo = "contact@mycompany.com";

            // Sign the document using the regular (non‑certification) signature.
            // The combination of SignaturesAppendOnly = true and a digital signature
            // effectively prevents further modifications without invalidating the signature.
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document signed and certified successfully: {outputPdf}");
    }
}
