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
        const string pfxFile = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxFile))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxFile}");
            return;
        }

        // Load the PDF document (using ensures disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle for the signature field (llx, lly, urx, ury)
            Rectangle rect = new Rectangle(100, 100, 250, 150);

            // Create a signature field on the first page and add it to the page annotations
            SignatureField sigField = new SignatureField(doc, rect);
            doc.Pages[1].Annotations.Add(sigField);

            // Create a concrete PKCS7 signature (Signature is abstract)
            PKCS7 pkcs7 = new PKCS7(pfxFile, pfxPassword)
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7);

            // Flatten the document so the signature appearance becomes a static part of the page
            doc.Flatten();

            // Save the signed and flattened PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPdf}'.");
    }
}
