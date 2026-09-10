using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed.pdf";
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Enable incremental updates so that further signatures can be added later
            doc.Form.SignaturesAppendOnly = true;

            // Define the rectangle (llx, lly, urx, ury) where the signature field will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page (using the Document constructor overload)
            SignatureField sigField = new SignatureField(doc, rect);
            sigField.PartialName = "Signature1"; // optional field name

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // Create a PKCS#7 signature object using the certificate (PFX) file
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
            pkcs7.Reason = "Approved";
            pkcs7.Location = "Office";
            pkcs7.ContactInfo = "contact@example.com";

            // Sign the document using the created signature field
            sigField.Sign(pkcs7);

            // Save the document. No SaveOptions are provided, so an incremental update is used.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document signed and saved to '{outputPdf}'. Additional signatures can be added later.");
    }
}