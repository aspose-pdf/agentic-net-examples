using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_flattened.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
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
            // Define the rectangle for the signature field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField);

            // Create a concrete PKCS7 signature object from a PFX file
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document approved",
                Location = "Office"
            };

            // Sign the PDF using the signature field
            sigField.Sign(pkcs7);

            // Flatten the signature field to make its appearance immutable
            sigField.Flatten();

            // Save the signed and flattened PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPdf}'.");
    }
}
