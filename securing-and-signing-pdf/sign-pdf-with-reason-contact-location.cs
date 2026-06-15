using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature field will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the document (first page by default)
            SignatureField sigField = new SignatureField(doc, rect);
            sigField.Name = "Signature1";

            // Add the signature field to the page's annotations collection
            doc.Pages[1].Annotations.Add(sigField);

            // Create a PKCS7 signature object using the PFX certificate
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
            pkcs7.Reason = "Approved for release";
            pkcs7.ContactInfo = "john.doe@example.com";
            pkcs7.Location = "New York, USA";

            // Sign the document using the signature field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}