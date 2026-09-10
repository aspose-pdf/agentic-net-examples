using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath  = "certificate.pfx";
        const string certPass  = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Create a visible signature field on the first page
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);
            // Optional visual properties
            sigField.Color = Aspose.Pdf.Color.LightGray;
            sigField.Contents = "Signature";
            // Add the signature field to the page annotations collection
            doc.Pages[1].Annotations.Add(sigField);

            // -----------------------------------------------------------------
            // 2. Prepare the signature object (PKCS#1 in this example)
            // -----------------------------------------------------------------
            PKCS1 pkcs1Signature = new PKCS1(certPath, certPass);
            pkcs1Signature.Reason   = "Document approval";
            pkcs1Signature.ContactInfo = "john.doe@example.com";
            pkcs1Signature.Location = "New York, USA";
            pkcs1Signature.Date = DateTime.UtcNow;

            // -----------------------------------------------------------------
            // 3. Sign the document using the signature field
            // -----------------------------------------------------------------
            sigField.Sign(pkcs1Signature);

            // -----------------------------------------------------------------
            // 4. Embed custom XMP metadata describing the signing process
            // -----------------------------------------------------------------
            // Document.Metadata provides access to XMP metadata (core API)
            var xmp = doc.Metadata;
            // Register a custom namespace (prefix, uri)
            xmp.RegisterNamespaceUri("custom", "http://example.com/custom/");
            // Add custom properties using the indexer
            xmp["custom:SigningProcess"] = "Signed using Aspose.Pdf core API with PKCS#1 signature";
            xmp["custom:SigningDate"] = DateTime.UtcNow.ToString("o");

            // -----------------------------------------------------------------
            // 5. Save the signed PDF
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
