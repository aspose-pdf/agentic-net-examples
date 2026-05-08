using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the output PDF, and the certificate (PFX) file.
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "yourPfxPassword";

        // Verify that the input files exist.
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
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Define the rectangle where the visible signature will appear.
            // Coordinates are in points (1/72 inch) with lower‑left origin.
            Aspose.Pdf.Rectangle signatureRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a signature field on the first page.
            SignatureField signatureField = new SignatureField(pdfDocument.Pages[1], signatureRect);

            // Add the signature field to the document's form collection.
            // The second argument is the 1‑based page number where the field resides.
            pdfDocument.Form.Add(signatureField, 1);

            // Create a PKCS#1 signature object using the RSA 4096‑bit certificate.
            // The constructor loads the certificate from the PFX file.
            PKCS1 pkcs1Signature = new PKCS1(certificatePath, certificatePassword);

            // Optional: set additional signature properties.
            pkcs1Signature.Reason      = "Approved for release";
            pkcs1Signature.ContactInfo = "security@example.com";
            pkcs1Signature.Location    = "Head Office";

            // Sign the document using the signature field.
            // The overload that accepts only the Signature object works because the certificate
            // is already associated with the PKCS1 instance.
            signatureField.Sign(pkcs1Signature);

            // Save the signed PDF.
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}