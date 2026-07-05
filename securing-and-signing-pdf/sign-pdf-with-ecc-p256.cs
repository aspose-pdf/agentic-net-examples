using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_output.pdf"; // signed PDF
        const string pfxPath    = "ecc_certificate.pfx"; // P‑256 ECC certificate
        const string pfxPassword = "password";          // certificate password

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the visible signature will appear
            // (left, bottom, width, height) – values are in points (1/72 inch)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 200, 100);

            // Create a signature field on the first page
            // SignatureField(Document, Rectangle) places the field on the first page by default
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                // Optional visual appearance settings
                Color = Aspose.Pdf.Color.LightGray,
                // The field name must be unique within the document
                Name = "Signature1"
            };
            // Add the field to the page's annotations collection
            doc.Pages[1].Annotations.Add(sigField);

            // Load the ECC certificate (P‑256 curve) from the PFX file
            X509Certificate2 cert = new X509Certificate2(pfxPath, pfxPassword,
                X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

            // Create an ExternalSignature which will use the ECC private key
            ExternalSignature eccSignature = new ExternalSignature(cert)
            {
                // Set signature metadata (optional)
                Reason      = "Document approved",
                ContactInfo = "john.doe@example.com",
                Location    = "New York, USA",
                Date        = DateTime.UtcNow
            };

            // Sign the document using the signature field and the ECC signature object
            sigField.Sign(eccSignature);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdf}'.");
    }
}