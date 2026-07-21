using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input / output PDF paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";

        // Path to the PFX certificate (replace with your actual certificate file)
        const string pfxFilePath = "certificate.pfx"; // The PFX file should be placed next to the executable or provide a full path
        const string pfxPassword = ""; // if the PFX is protected, set the password here

        // Load the PFX certificate into a stream
        if (!File.Exists(pfxFilePath))
        {
            Console.WriteLine($"Certificate file not found: {pfxFilePath}");
            return;
        }
        byte[] pfxBytes = File.ReadAllBytes(pfxFilePath);
        using var pfxStream = new MemoryStream(pfxBytes);

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a visible signature field on the first page
            var signatureRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
            var signatureField = new SignatureField(pdfDocument.Pages[1], signatureRect)
            {
                PartialName = "Signature1"
            };
            pdfDocument.Form.Add(signatureField, 1);

            // Prepare the PKCS#1 signature object using the retrieved certificate
            var pkcs1Signature = new PKCS1(pfxStream, pfxPassword)
            {
                Reason = "Document signed for compliance",
                ContactInfo = "signer@example.com",
                Location = "Head Office"
            };

            // Sign the PDF using the signature field
            signatureField.Sign(pkcs1Signature);

            // Save the signed PDF
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPdfPath}'.");
    }
}
