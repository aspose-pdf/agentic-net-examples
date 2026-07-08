using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Signatures;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the certificate and output PDF
        const string signedPdfPath = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePwd = "yourPassword";

        // Simple XML content – no external file required
        const string xmlContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<root><message>Hello World</message></root>";

        // Create a new PDF document and add the XML content as plain text
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Add the XML string to the page using a TextFragment
            TextFragment tf = new TextFragment(xmlContent)
            {
                // Optional: set font size and style for better readability
                TextState = { FontSize = 12 }
            };
            page.Paragraphs.Add(tf);

            // Define the rectangle where the signature will appear (llx, lly, urx, ury)
            var signatureRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            var sigField = new SignatureField(page, signatureRect)
            {
                PartialName = "Signature1"
            };
            // Add the field to the document's form collection (page 1)
            doc.Form.Add(sigField, 1);

            // Ensure the certificate file exists before attempting to sign
            if (!File.Exists(certificatePath))
            {
                Console.WriteLine($"Certificate file '{certificatePath}' not found. PDF will be saved without a digital signature.");
            }
            else
            {
                // Prepare a PKCS#7 signature using the PFX certificate file
                var pkcs7 = new PKCS7(certificatePath, certificatePwd)
                {
                    Reason = "Document approved",
                    Location = "Head Office"
                };

                // Sign the document using the signature field
                sigField.Sign(pkcs7);
            }

            // Save the (signed or unsigned) PDF
            doc.Save(signedPdfPath);
            Console.WriteLine($"PDF saved to '{signedPdfPath}'.");
        }
    }
}
