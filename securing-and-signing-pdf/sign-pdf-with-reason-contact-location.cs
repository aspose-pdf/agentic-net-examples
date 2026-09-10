using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and certificate (PFX) paths
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "pfxPassword";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            // Add the field to the page's annotation collection
            doc.Pages[1].Annotations.Add(sigField);

            // Create a PKCS#7 signature object using the PFX file
            PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword);

            // Populate the required signature properties
            pkcs7Signature.Reason      = "Approved for release";
            pkcs7Signature.ContactInfo = "john.doe@example.com";
            pkcs7Signature.Location    = "New York, USA";

            // Sign the document using the signature field
            sigField.Sign(pkcs7Signature);

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}