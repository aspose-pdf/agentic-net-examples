using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the signature field (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField signatureField = new SignatureField(doc.Pages[1], rect);
            signatureField.PartialName = "Signature1";
            signatureField.AlternateName = "Signature Field";

            // Add the signature field to the page annotations collection
            doc.Pages[1].Annotations.Add(signatureField);

            // Load the self‑signed certificate from a PFX file
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                // Create a PKCS#7 signature object using the certificate stream and password
                PKCS7 pkcs7Signature = new PKCS7(pfxStream, pfxPassword);
                pkcs7Signature.Reason = "Document approved";
                pkcs7Signature.Location = "Office";
                pkcs7Signature.ContactInfo = "contact@example.com";

                // Sign the document using the signature field
                signatureField.Sign(pkcs7Signature);
            }

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}