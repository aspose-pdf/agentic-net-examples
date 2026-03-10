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

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

            // Create a signature field on the first page
            SignatureField signatureField = new SignatureField(doc.Pages[1], sigRect);

            // Add the signature field to the document's form collection
            doc.Form.Add(signatureField);

            // Create a PKCS#1 signature object using the certificate file and password
            PKCS1 pkcs1Signature = new PKCS1(pfxPath, pfxPassword)
            {
                Reason   = "Document approved",
                Location = "Company HQ",
                Date     = DateTime.Now
            };

            // Apply the digital signature to the field
            signatureField.Sign(pkcs1Signature);

            // Save the signed PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}