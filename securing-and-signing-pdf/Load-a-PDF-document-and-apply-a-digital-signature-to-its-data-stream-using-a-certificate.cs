using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
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
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the signature will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the page
            SignatureField signatureField = new SignatureField(page, rect);

            // Load the certificate (PFX) as a stream
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                // Create a PKCS7 signature object using the certificate stream
                PKCS7 signature = new PKCS7(pfxStream, pfxPassword);

                // Optional: set additional signature properties
                signature.Reason = "Document approved";
                signature.Location = "Head Office";

                // Apply the digital signature to the field
                signatureField.Sign(signature);
            }

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}