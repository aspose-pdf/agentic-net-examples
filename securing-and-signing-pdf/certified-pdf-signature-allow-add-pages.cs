using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, and certificate (PFX) paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the certification signature will appear
            // Fully qualified type to avoid ambiguity
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);
            sigField.Name = "CertSignature";
            doc.Form.Add(sigField, 1);

            // Create a PKCS#1 signature object using the certificate
            PKCS1 pkcs1Signature = new PKCS1(certificatePath, certificatePassword);
            pkcs1Signature.Reason = "Document certified – new pages may be added";
            pkcs1Signature.ContactInfo = "contact@example.com";
            pkcs1Signature.Location = "Head Office";

            // Sign the document using the signature field
            // This creates a certification (MDP) signature when the field is empty
            sigField.Sign(pkcs1Signature);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPdf}'.");
    }
}