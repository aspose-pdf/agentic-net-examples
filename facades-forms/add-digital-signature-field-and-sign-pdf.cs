using System;
using System.IO;
using System.Drawing;                     // Required for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;                // PdfFileSignature, FormEditor
using Aspose.Pdf.Forms;                  // FieldType, PKCS1

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, certificate path and password
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

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

        try
        {
            // ------------------------------------------------------------
            // 1. Add a signature field named "DigitalSignature" to page 1
            // ------------------------------------------------------------
            // Load the source PDF into a Document instance (required by older FormEditor overloads).
            Document srcDoc = new Document(inputPdf);
            FormEditor formEditor = new FormEditor(srcDoc);
            // Parameters: field type, field name, page number (1‑based), llx, lly, urx, ury
            formEditor.AddField(FieldType.Signature, "DigitalSignature", 1, 100f, 100f, 250f, 150f);
            // Save the document that now contains the empty signature field.
            formEditor.Save(outputPdf);

            // ------------------------------------------------------------
            // 2. Sign the newly added field using a PKCS#1 signature
            // ------------------------------------------------------------
            // Load the PDF that contains the signature field.
            Document docToSign = new Document(outputPdf);
            PdfFileSignature pdfSigner = new PdfFileSignature();
            pdfSigner.BindPdf(docToSign);

            // Create the PKCS#1 signature object.
            PKCS1 pkcs1Signature = new PKCS1(certPath, certPass)
            {
                Reason      = "Document approval",
                ContactInfo = "john.doe@example.com",
                Location    = "New York"
            };

            // Define the visible rectangle for the signature appearance.
            // Rectangle constructor: (x, y, width, height)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 150, 50);

            // Use the overload that matches the library version: (int pageNumber, string reason,
            // string contactInfo, string location, bool isVisible, Rectangle rectangle, PKCS1 pkcs1)
            pdfSigner.Sign(
                1,
                pkcs1Signature.Reason,
                pkcs1Signature.ContactInfo,
                pkcs1Signature.Location,
                true,
                rect,
                pkcs1Signature);

            // Save the signed PDF (overwrites the same file).
            pdfSigner.Save(outputPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
