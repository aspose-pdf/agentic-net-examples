using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_output.pdf"; // signed PDF
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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document allows incremental updates for additional signatures
            doc.Form.SignaturesAppendOnly = true;

            // Define the rectangle where the signature field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            sigField.PartialName = "Signature1"; // field name
            sigField.AlternateName = "Signature Field"; // tooltip
            doc.Form.Add(sigField);

            // Create a PKCS#7 signature using the certificate file
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword)
            {
                Reason   = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com",
                Date = DateTime.UtcNow
            };

            // Sign the document using the created signature field
            sigField.Sign(pkcs7Signature);

            // Save the document. No SaveOptions are provided, so an incremental update is used.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPdf}'. Additional signatures can be added later.");
    }
}