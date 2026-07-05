using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Add a signature field on page 1 (1‑based indexing)
            Aspose.Pdf.Facades.FormEditor formEditor = new Aspose.Pdf.Facades.FormEditor(doc);
            // Parameters: field type, field name, page number, llx, lly, urx, ury
            formEditor.AddField(Aspose.Pdf.Facades.FieldType.Signature, "Signature1", 1, 100, 100, 250, 150);
            // The field is now part of the document

            // Create a PKCS#1 signature object with the certificate
            Aspose.Pdf.Forms.PKCS1 signature = new Aspose.Pdf.Forms.PKCS1(certPath, certPassword)
            {
                Reason = "Approved",
                ContactInfo = "john.doe@example.com",
                Location = "New York"
            };

            // Sign the document using the previously added signature field
            Aspose.Pdf.Facades.PdfFileSignature pdfSigner = new Aspose.Pdf.Facades.PdfFileSignature(doc);
            // Optional: set an image that will be used as the visual appearance of the signature
            // pdfSigner.SignatureAppearance = "signature.png";
            pdfSigner.Sign("Signature1", signature);

            // Save the signed PDF
            pdfSigner.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}