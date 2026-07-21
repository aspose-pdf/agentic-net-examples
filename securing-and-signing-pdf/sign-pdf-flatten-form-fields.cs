using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added for Border class

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_flattened.pdf"; // result PDF
        const string certPath   = "certificate.pfx";   // signing certificate
        const string certPass   = "password";          // certificate password

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature field will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            // Optional visual styling
            sigField.Color = Aspose.Pdf.Color.LightGray;
            sigField.Border = new Border(sigField) { Width = 1 };

            // Add the signature field to the page's annotations collection
            doc.Pages[1].Annotations.Add(sigField);

            // Create a PKCS#7 signature object using the certificate file
            PKCS7 pkcs7Signature = new PKCS7(certPath, certPass)
            {
                Reason   = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com",
                Authority = "John Doe"
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7Signature);

            // After signing, flatten all form fields to prevent further editing
            doc.Flatten();

            // Save the signed and flattened PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPdf}'.");
    }
}
