using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – replace with actual file locations
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePfx = "certificate.pfx";
        const string pfxPassword    = "password";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the certificate file exists
        if (!File.Exists(certificatePfx))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePfx}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            doc.Pages[1].Annotations.Add(sigField); // Attach the field to the page

            // Create a PKCS#7 signature object using the PFX certificate
            PKCS7 pkcs7Signature = new PKCS7(certificatePfx, pfxPassword);

            // Populate signature metadata
            pkcs7Signature.Reason      = "I agree to the terms of this document.";
            pkcs7Signature.ContactInfo = "john.doe@example.com";
            pkcs7Signature.Location    = "New York, USA";

            // Sign the document using the signature field
            sigField.Sign(pkcs7Signature);

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}