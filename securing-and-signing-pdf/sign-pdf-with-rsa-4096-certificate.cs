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

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle for the signature appearance (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page and add it to the page annotations
            SignatureField signatureField = new SignatureField(doc.Pages[1], rect);
            doc.Pages[1].Annotations.Add(signatureField);

            // Load the RSA 4096‑bit certificate (PFX) and create a PKCS7 signature object
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                PKCS7 pkcs7 = new PKCS7(pfxStream, pfxPassword);

                // Set optional signature properties on the PKCS7 object (not on the field)
                pkcs7.Reason = "Document approved";
                pkcs7.Location = "Company HQ";
                pkcs7.ContactInfo = "contact@example.com";
                pkcs7.Authority = "John Doe"; // optional, maps to the signer name

                // Sign the field (lifecycle: create signature, then sign)
                signatureField.Sign(pkcs7);
            }

            // Save the signed PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
