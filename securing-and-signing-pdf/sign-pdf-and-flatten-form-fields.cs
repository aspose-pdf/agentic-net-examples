using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_flattened.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

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
            // Define the rectangle where the signature field will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page
            SignatureField signatureField = new SignatureField(doc, rect)
            {
                PartialName = "Signature1"
            };

            // Add the signature field to the page annotations collection
            doc.Pages[1].Annotations.Add(signatureField);

            // Create a PKCS7 signature using a certificate file and password
            Signature signature = new PKCS7(certPath, certPassword)
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the document using the signature field
            signatureField.Sign(signature);

            // Flatten all form fields so their values become part of the page content
            doc.Flatten();

            // Save the signed and flattened PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPdf}'.");
    }
}