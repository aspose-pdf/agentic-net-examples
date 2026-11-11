using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Signatures; // for PKCS7

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certificate = "certificate.pfx";
        const string certPassword = "password";
        const int signaturePage = 1; // 1‑based page number
        const string fieldName = "Signature1";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificate))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificate}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPdf);

        // Define the rectangle that will bound the signature field.
        // Constructor: (llx, lly, urx, ury)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

        // Create a signature field on the required page.
        SignatureField sigField = new SignatureField(doc.Pages[signaturePage], rect)
        {
            PartialName = fieldName
        };

        // Add the field to the form (page index is 1‑based).
        doc.Form.Add(sigField, signaturePage);

        // Initialise the PdfFileSignature and bind the document.
        using (PdfFileSignature signer = new PdfFileSignature())
        {
            signer.BindPdf(doc);

            // Create PKCS7 signature object.
            PKCS7 pkcs = new PKCS7(certificate, certPassword);

            // Sign using the previously created field name.
            signer.Sign(fieldName, pkcs);

            // Save the signed PDF.
            signer.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
