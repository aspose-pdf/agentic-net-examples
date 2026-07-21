using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string certPath      = "certificate.pfx";
        const string certPassword  = "password";

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        try
        {
            // Load PDF from a file stream
            using (FileStream pdfStream = File.OpenRead(inputPdfPath))
            using (Document pdfDoc = new Document(pdfStream))
            {
                // Create a signature field on the first page
                // Rectangle(left, bottom, width, height)
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 200, 150);
                SignatureField sigField = new SignatureField(pdfDoc.Pages[1], sigRect)
                {
                    PartialName = "Signature1"
                };
                // Add the signature field to the document's form collection
                pdfDoc.Form.Add(sigField);

                // Prepare the PKCS#7 detached signature object
                using (FileStream certStream = File.OpenRead(certPath))
                {
                    // Use the constructor that takes a certificate stream and password
                    PKCS7Detached pkcs7 = new PKCS7Detached(certStream, certPassword);

                    // Set optional signature properties
                    pkcs7.Reason      = "Document approved";
                    pkcs7.Location    = "New York, USA";
                    pkcs7.ContactInfo = "john.doe@example.com";
                    pkcs7.Date        = DateTime.UtcNow;

                    // Sign the document using the signature field
                    sigField.Sign(pkcs7);
                }

                // Save the signed PDF
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}