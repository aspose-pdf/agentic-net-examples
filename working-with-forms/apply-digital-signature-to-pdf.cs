using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "filled_form.pdf";      // PDF with filled form fields
        const string outputPdfPath = "signed_form.pdf";    // Resulting PDF with a visible signature field
        const string signatureImagePath = "signature.png"; // Optional visual signature image (can be null)

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        bool useImage = File.Exists(signatureImagePath);

        try
        {
            // Load the PDF that already contains filled form fields
            Document pdfDocument = new Document(inputPdfPath);

            // -----------------------------------------------------------------
            // Create a visible signature field on the first page (adjust rectangle as needed)
            // -----------------------------------------------------------------
            const int pageNumber = 1; // Aspose pages are 1‑based
            const string signatureFieldName = "Signature1";
            // Rectangle(left, bottom, right, top) – coordinates are in points (1/72 inch)
            var signatureRect = new Rectangle(100, 100, 300, 150);

            // The SignatureField constructor expects a Page (or Document) and a Rectangle.
            // The field is visible by default; no IsVisible property exists.
            var signatureField = new SignatureField(pdfDocument.Pages[pageNumber], signatureRect)
            {
                Name = signatureFieldName
            };

            // If a visual image is supplied, store its path for later use when a real
            // cryptographic signature is applied (requires the Facades API, which is
            // intentionally omitted here). The core API does not expose a per‑field
            // graphic property.
            if (useImage)
            {
                // Placeholder comment – the image can be used via PKCS7.CustomAppearance
                // when a digital signature is added with the Facades API.
            }

            // Add the signature field to the document's form collection
            pdfDocument.Form.Add(signatureField, pageNumber);

            // -----------------------------------------------------------------
            // NOTE:
            // Actual digital signing (PKCS#7/PKCS#1) is performed via the
            // Aspose.Pdf.Facades.PdfFileSignature class, which is outside the
            // allowed core API namespace for this exercise. Therefore this
            // example only creates a visible signature field without applying
            // a cryptographic signature.
            // -----------------------------------------------------------------

            // Save the PDF with the new signature field
            pdfDocument.Save(outputPdfPath);

            Console.WriteLine($"Signature field added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
