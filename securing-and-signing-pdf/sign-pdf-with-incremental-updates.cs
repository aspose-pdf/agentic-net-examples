using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "certPassword";

        // Verify required files exist
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
        Document pdfDocument = new Document(inputPdfPath);

        // Define a rectangle for the visible signature (optional)
        // Aspose.Pdf.Rectangle uses lower‑left (xLL, yLL) and upper‑right (xUR, yUR) coordinates.
        // Here we place the signature at (100,100) with a width of 200 and height of 50 points.
        var signatureRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

        // Create a signature field on the first page and add it to the form
        var signatureField = new SignatureField(pdfDocument.Pages[1], signatureRect)
        {
            PartialName = "Approver"
        };
        pdfDocument.Form.Add(signatureField, 1);

        // Prepare the PKCS7 signature object with metadata
        var pkcs7 = new PKCS7(certificatePath, certificatePassword)
        {
            Reason = "Document approved",
            Location = "Head Office",
            ContactInfo = "contact@example.com",
            CustomAppearance = new SignatureCustomAppearance
            {
                FontSize = 8,
                ShowContactInfo = false,
                ShowLocation = false
            }
        };

        // Sign the field. This writes the digital signature into the PDF stream.
        signatureField.Sign(pkcs7);

        // Save the signed PDF.
        pdfDocument.Save(outputPdfPath);

        Console.WriteLine($"PDF signed successfully. Output saved to '{outputPdfPath}'.");
    }
}
