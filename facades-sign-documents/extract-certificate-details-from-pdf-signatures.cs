using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(pdfPath);

        // Verify that the document contains a form
        if (pdfDoc.Form == null)
        {
            Console.WriteLine("The PDF does not contain any form.");
            return;
        }

        // Verify that the form contains fields
        if (pdfDoc.Form.Fields == null || !pdfDoc.Form.Fields.Any())
        {
            Console.WriteLine("No form fields (including signature fields) found in the PDF.");
            return;
        }

        // Iterate over each form field and process signature fields
        foreach (var field in pdfDoc.Form.Fields)
        {
            if (field is SignatureField sigField)
            {
                // Extract the certificate associated with the signature field
                X509Certificate2 cert = sigField.ExtractCertificateObject();

                Console.WriteLine($"Signature Field: {sigField.PartialName}");
                if (cert != null)
                {
                    Console.WriteLine($"  Issuer          : {cert.Issuer}");
                    Console.WriteLine($"  Expiration Date: {cert.NotAfter}");
                }
                else
                {
                    Console.WriteLine("  No certificate associated with this signature.");
                }
            }
        }
    }
}
