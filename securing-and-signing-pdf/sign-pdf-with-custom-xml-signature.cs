using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

        // Custom XML signature data to be embedded
        const string customXml = "<Signature><SignedBy>John Doe</SignedBy><Date>2024-07-04</Date></Signature>";
        byte[] customXmlBytes = Encoding.UTF8.GetBytes(customXml);

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdf))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Define the rectangle where the signature field will appear (left, bottom, width, height)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 200, 50);

            // Create a signature field on the first page
            Aspose.Pdf.Forms.SignatureField sigField = new Aspose.Pdf.Forms.SignatureField(doc.Pages[1], sigRect);
            doc.Form.Add(sigField);

            // Create a PKCS#7 signature object using the certificate
            Aspose.Pdf.Forms.PKCS7 pkcs7 = new Aspose.Pdf.Forms.PKCS7(certPath, certPass);
            pkcs7.Reason      = "Document approved";
            pkcs7.ContactInfo = "john.doe@example.com";
            pkcs7.Location    = "New York, USA";

            // Embed custom XML data by providing a custom signing delegate.
            // The delegate receives the document hash and the digest algorithm.
            // We ignore both parameters and return the pre‑computed XML bytes.
            pkcs7.CustomSignHash = (byte[] hash, DigestHashAlgorithm alg) => customXmlBytes;

            // Sign the field with the prepared signature object
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
