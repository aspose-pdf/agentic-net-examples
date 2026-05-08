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
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle for the signature field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, rect);

            // Add the signature field to the page's annotation collection
            doc.Pages[1].Annotations.Add(sigField);

            // Create a concrete PKCS7 signature object from a PFX file
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
            pkcs7.Reason = "Document approved";
            pkcs7.Location = "Office";
            // Optional: pkcs7.ContactInfo = "contact@example.com";

            // Sign the document using the signature field
            sigField.Sign(pkcs7);

            // Flatten the signature appearance to prevent further visual changes
            sigField.Flatten();

            // Save the signed and flattened PDF (lifecycle rule: use Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPdf}'.");
    }
}
