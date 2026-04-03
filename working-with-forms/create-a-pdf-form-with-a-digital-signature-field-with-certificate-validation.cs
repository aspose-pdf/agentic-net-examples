using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPdf = "signed_form.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "pfxPassword";

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        using (Document doc = new Document())
        {
            // Add a blank page – required before placing a signature field
            Page page = doc.Pages.Add();

            // Define the rectangle for the signature field (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            var sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field
            var sigField = new SignatureField(doc, sigRect)
            {
                Name = "MySignature",
                AlternateName = "Signature Field"
            };
            page.Annotations.Add(sigField);

            // Load the certificate into a concrete PKCS7 object (Signature is abstract)
            PKCS7 pkcs7;
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                pkcs7 = new PKCS7(pfxStream, pfxPassword);
            }

            // Optional signature properties
            pkcs7.Reason = "Document approval";
            pkcs7.Location = "Office";
            pkcs7.ContactInfo = "contact@example.com";
            pkcs7.Date = DateTime.UtcNow;

            // Sign the field with the PKCS7 signature
            sigField.Sign(pkcs7);

            // Verify the signature (Signature property is populated after signing)
            bool isValid = sigField.Signature != null && sigField.Signature.Verify();
            Console.WriteLine($"Signature verification result: {(isValid ? "Valid" : "Invalid")}");

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
