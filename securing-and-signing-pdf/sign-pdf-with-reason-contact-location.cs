using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputPdfPath = "signed_output.pdf"; // signed PDF
        const string pfxPath       = "certificate.pfx";   // signing certificate
        const string pfxPassword   = "pfxPassword";       // certificate password

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the certificate file exists
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate not found: {pfxPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will appear (llx, lly, urx, ury)
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            doc.Pages[1].Annotations.Add(sigField);

            // Create a PKCS#7 signature object using the PFX file
            // This constructor loads the certificate and prepares it for signing
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword);

            // Populate the required signature properties
            pkcs7Signature.Reason      = "Approved for release";
            pkcs7Signature.ContactInfo = "john.doe@example.com";
            pkcs7Signature.Location    = "New York, USA";

            // Sign the document using the signature field
            sigField.Sign(pkcs7Signature);

            // Save the signed PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}