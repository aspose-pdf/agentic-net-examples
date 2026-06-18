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
        const string pfxPath = "ecc_key.pfx";
        const string pfxPassword = "password";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField);

            // Load the ECC certificate (P‑256) from the PFX file
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                // PKCS7 will automatically use the key algorithm from the certificate (ECDSA for ECC)
                PKCS7 pkcs7 = new PKCS7(pfxStream, pfxPassword)
                {
                    Reason = "Document approved",
                    Location = "Head Office",
                    ContactInfo = "contact@example.com"
                };

                // Sign the signature field with the ECC certificate
                sigField.Sign(pkcs7);
            }

            // Save the signed PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}