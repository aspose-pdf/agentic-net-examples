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

        // Open the PFX containing an ECC P‑256 private key
        using (FileStream pfxStream = File.OpenRead(pfxPath))
        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the signature field will be placed
            Page page = doc.Pages[1];

            // Define the signature field rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field and add it to the document form
            SignatureField signatureField = new SignatureField(page, rect);
            signatureField.PartialName = "Signature1";
            doc.Form.Add(signatureField);

            // Create a PKCS7 signature object using the ECC certificate
            PKCS7 pkcs7 = new PKCS7(pfxStream, pfxPassword);
            pkcs7.Reason = "Document approved";
            pkcs7.Location = "Office";
            pkcs7.ContactInfo = "email@example.com";

            // Sign the field with the ECC key (compact signature size)
            signatureField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPdf}'.");
    }
}