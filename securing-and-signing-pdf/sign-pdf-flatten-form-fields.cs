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
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

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
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField signatureField = new SignatureField(doc, rect);
            doc.Pages[1].Annotations.Add(signatureField);

            // Create a PKCS#7 signature using the certificate (concrete class, not abstract Signature)
            PKCS7 signature = new PKCS7(certPath, certPassword);
            signature.Reason = "Document approved";
            signature.Location = "Company HQ";

            // Sign the document using the signature field
            signatureField.Sign(signature);

            // Flatten all form fields to prevent further editing
            doc.Flatten();

            // Save the signed and flattened PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPdf}'.");
    }
}
