using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

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

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will appear (llx, lly, urx, ury)
            Rectangle sigRect = new Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            doc.Pages[1].Annotations.Add(sigField);

            // Initialize the concrete PKCS7 signature object with the self‑signed certificate
            PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword)
            {
                Reason = "Approved",
                Location = "Head Office",
                ContactInfo = "admin@example.com"
            };

            // Sign the document using the created signature field
            sigField.Sign(pkcs7Signature);

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
