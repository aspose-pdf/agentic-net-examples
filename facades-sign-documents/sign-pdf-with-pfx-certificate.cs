using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // for PKCS1 if needed

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the PdfFileSignature facade
            PdfFileSignature signer = new PdfFileSignature();
            // Bind the PDF document to the facade
            signer.BindPdf(doc);
            // Set the certificate and its password
            signer.SetCertificate(pfxPath, pfxPassword);
            // Optional: set a visual appearance image for the signature
            // signer.SignatureAppearance = "signature_appearance.png";

            // Define the rectangle where the visible signature will be placed
            // System.Drawing.Rectangle is required by the Sign method
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Sign page 1 with reason, contact, location, visibility flag and rectangle
            signer.Sign(1, "Document approved", "John Doe", "New York", true, rect);

            // Save the signed PDF
            signer.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}