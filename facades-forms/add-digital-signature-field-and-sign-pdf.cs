using System;
using System.IO;
using System.Drawing; // for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf = "input.pdf";
        const string tempPdf = "temp_with_field.pdf"; // intermediate file with the signature field
        const string outputPdf = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPass = "password";
        const string appearanceImage = "signature_appearance.png"; // optional appearance image

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

        // ------------------------------------------------------------
        // 1. Add a signature field named "DigitalSignature" to the PDF.
        //    FormEditor works on a Document instance; use the overload that
        //    accepts a Document and persist the changes with Save(destination).
        // ------------------------------------------------------------
        Document doc = new Document(inputPdf);
        FormEditor formEditor = new FormEditor(doc);
        // Add a signature field on page 1, positioned at (100,100) lower‑left
        // and (200,150) upper‑right (coordinates are in points).
        formEditor.AddField(FieldType.Signature, "DigitalSignature", 1, 100f, 100f, 200f, 150f);
        // Save the document that now contains the empty signature field.
        formEditor.Save(tempPdf);

        // ------------------------------------------------------------
        // 2. Sign the newly added field using PdfFileSignature.
        //    Use the overload that accepts reason, contact, location, a
        //    visibility flag, a Rectangle describing the visible area and
        //    a PKCS1 certificate object.
        // ------------------------------------------------------------
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(tempPdf);

        // Optional: set a graphic appearance for the visible signature.
        if (File.Exists(appearanceImage))
            pdfSigner.SignatureAppearance = appearanceImage;

        // Create a PKCS#1 signature object – do NOT set Reason/Contact/Location on it.
        PKCS1 pkcs1 = new PKCS1(certPath, certPass);

        // Rectangle for the visible signature (x, y, width, height).
        // The field we added occupies (100,100) – (200,150).
        System.Drawing.Rectangle visibleRect = new System.Drawing.Rectangle(100, 100, 100, 50);

        // Sign the field. Use positional arguments to match the overload that includes the page number.
        pdfSigner.Sign(
            1,                     // page number (1‑based)
            "Document approved", // reason
            "contact@example.com", // contact info
            "Office",             // location
            true,                  // visible signature flag
            visibleRect,           // signature rectangle
            pkcs1);                // PKCS#1 object

        // Save the final signed PDF.
        pdfSigner.Save(outputPdf);

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
