using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_flattened.pdf";
        const string pfxPath    = "certificate.pfx";
        const string pfxPassword = "pfxPassword";

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
            // Define the rectangle for the signature field (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, rect)
            {
                PartialName = "Signature1"
            };
            // Add the field to the document's form collection
            doc.Form.Add(sigField);

            // Create a concrete PKCS#7 signature object (Signature is abstract)
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document signed",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7);

            // Flatten the document to embed the signature appearance permanently
            doc.Flatten();

            // Save the signed and flattened PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPdf}'.");
    }
}
