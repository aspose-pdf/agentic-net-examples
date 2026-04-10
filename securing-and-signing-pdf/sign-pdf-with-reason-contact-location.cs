using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "signed_output.pdf";  // signed PDF
        const string pfxPath   = "certificate.pfx";   // signing certificate
        const string pfxPassword = "pfxPassword";     // certificate password

        // Ensure source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            sigField.PartialName = "Signature1"; // optional field name
            doc.Pages[1].Annotations.Add(sigField);

            // Create a PKCS#7 signature object using the PFX file
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword);

            // Populate the required properties
            pkcs7Signature.Reason      = "Approved for release";
            pkcs7Signature.ContactInfo = "john.doe@example.com";
            pkcs7Signature.Location    = "New York, USA";

            // Sign the document using the signature field
            sigField.Sign(pkcs7Signature);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdf}'.");
    }
}