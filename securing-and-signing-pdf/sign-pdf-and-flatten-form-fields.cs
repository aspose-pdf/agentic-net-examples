using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_flattened.pdf";
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

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a PKCS7 signature object using the provided certificate.
            PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
            {
                // Optional: set reason, location, contact info. Empty strings suppress the visual text.
                Reason = string.Empty,
                Location = string.Empty,
                ContactInfo = string.Empty
            };

            // Define where the visible signature will appear on the first page.
            // Aspose.Pdf.Rectangle expects (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y).
            Aspose.Pdf.Rectangle signatureRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page.
            SignatureField signatureField = new SignatureField(pdfDoc.Pages[1], signatureRect)
            {
                PartialName = "Signature1"
            };

            // Add the field to the document's form collection.
            pdfDoc.Form.Add(signatureField);

            // Sign the field with the PKCS7 object.
            signatureField.Sign(pkcs7);

            // Flatten the document so that all form fields (including the signature) become non‑editable.
            pdfDoc.Flatten();

            // Save the signed and flattened PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPdfPath}'.");
    }
}
