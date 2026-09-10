using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "filled_form.pdf";
        const string outputPath = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Define the rectangle for the signature field (left, bottom, width, height)
            Rectangle sigRect = new Rectangle(100, 100, 250, 150);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                Name = "Signature1"
            };

            // Add the signature field to the form collection
            doc.Form.Add(sigField);

            // Create a concrete PKCS7 signature object using the PFX certificate
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document certification",
                Location = "Company HQ",
                ContactInfo = "contact@example.com"
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
