using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_certified.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document doc = new Document(inputPath);

            // Create a PKCS7 signature object (core API)
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Document certified - no changes allowed",
                Location = "Company HQ",
                ContactInfo = "contact@example.com"
            };

            // Optional: define a custom (invisible) appearance for the signature
            pkcs7.CustomAppearance = new SignatureCustomAppearance
            {
                // Transparent background makes the signature invisible
                BackgroundColor = Aspose.Pdf.Color.Transparent
            };

            // Create an invisible signature field on the first page
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                PartialName = "CertSignature"
            };
            doc.Form.Add(sigField, 1);

            // Sign the field using the PKCS7 credentials
            sigField.Sign(pkcs7);

            // NOTE: The core Aspose.Pdf API does not expose a direct way to set a
            // certification level (DocMDP) that blocks further modifications.
            // Certification is performed via the PdfFileSignature facade (Aspose.Pdf.Facades)
            // using DocMDPSignature. Because the task explicitly forbids any
            // Aspose.Pdf.Facades usage, this example limits itself to a regular
            // digital signature.

            // Save the signed PDF
            doc.Save(outputPath);

            Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
