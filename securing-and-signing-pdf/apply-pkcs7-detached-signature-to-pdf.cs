using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputPdfPath  = "signed.pdf";         // signed PDF
        const string certificatePath = "certificate.pfx";   // PKCS#12 certificate
        const string certificatePassword = "password";      // certificate password

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

        // Load the PDF from a file stream (core API, no Facades)
        using (FileStream pdfStream = File.OpenRead(inputPdfPath))
        using (Document doc = new Document(pdfStream))
        {
            // Create a signature field on the first page.
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 200, 150);
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"   // field identifier
            };
            // Add the field to the document's form collection.
            doc.Form.Add(sigField);

            // Create a PKCS#7 detached signature object.
            // Using the (string pfx, string password) ctor embeds the certificate info.
            PKCS7Detached pkcs7 = new PKCS7Detached(certificatePath, certificatePassword)
            {
                Reason      = "Approved for release",
                Location    = "New York",
                ContactInfo = "john.doe@example.com",
                // Optional: set appearance properties, timestamps, etc.
                ShowProperties = true
            };

            // Sign the field. The overload without certificate parameters works because
            // the PKCS7Detached instance already contains the certificate data.
            sigField.Sign(pkcs7);

            // Save the signed PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}