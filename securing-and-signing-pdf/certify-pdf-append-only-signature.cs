using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputPdf = "signed_output.pdf"; // signed PDF
        const string pfxPath = "certificate.pfx";    // PKCS#12 certificate file
        const string pfxPassword = "password";       // certificate password

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
            // Allow incremental updates so annotations can be added after signing
            doc.Form.SignaturesAppendOnly = true;

            // Define the rectangle where the signature field will appear
            var sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a signature field
            var sigField = new SignatureField(doc, sigRect);

            // Add the signature field to the first page (page numbers are 1‑based)
            doc.Form.Add(sigField, 1);

            // Create a concrete PKCS#7 signature (Signature is abstract)
            var pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document certified – annotations allowed",
                Location = "Company HQ",
                ContactInfo = "contact@example.com",
                Date = DateTime.Now
            };

            // Sign the field with the PKCS#7 signature
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document signed and saved to '{outputPdf}'.");
    }
}
