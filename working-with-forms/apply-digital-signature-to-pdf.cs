using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certPath       = "certificate.pfx";
        const string certPassword   = "certPassword";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document (contains already filled form fields)
        using (Document doc = new Document(inputPdfPath))
        {
            // OPTIONAL: set form field values programmatically
            // Example: doc.Form["NameField"].Value = "John Doe";

            // Create a signature field on the first page
            // Rectangle coordinates are in points (lower‑left x, lower‑right y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
            SignatureField signature = new SignatureField(doc.Pages[1], sigRect)
            {
                PartialName = "Signature1",
                Name        = "John Doe"
            };

            // Add the signature field to the document's form collection
            doc.Form.Add(signature);

            // Load the signing certificate (PKCS#12 / .pfx) and create a PKCS7 signature object
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword);
            pkcs7.Reason      = "Document approval";
            pkcs7.Location    = "New York, USA";
            pkcs7.ContactInfo = "john.doe@example.com";

            // Apply the digital signature to the field
            signature.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}
