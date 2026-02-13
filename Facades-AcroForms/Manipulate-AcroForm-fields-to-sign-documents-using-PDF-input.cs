using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        // Generic file names – adjust paths if needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";

        try
        {
            // Verify required files exist
            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Error: Input PDF not found – {inputPdfPath}");
                return;
            }

            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Define the rectangle for the signature field (llx, lly, urx, ury)
            var signatureRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

            // Create a signature field on the first page
            var signatureField = new SignatureField(pdfDocument.Pages[1], signatureRect)
            {
                AlternateName = "Signature",   // Tooltip text
                PartialName   = "Signature1", // Internal field name
                Required      = true           // Mark as required
            };

            // Add the field to the document's form collection
            pdfDocument.Form.Add(signatureField);

            // NOTE: The concrete PKCS#7 signature object (PKCS7Signature) is part of the
            // Aspose.Pdf.Signatures package. If that package is not referenced, the type
            // is unavailable and the Signature property of SignatureField is read‑only.
            // For a pure, cross‑platform example we only create the empty signature field.
            // To apply a real digital signature, reference the Aspose.Pdf.Signatures DLL and
            // assign a PKCS7Signature instance to the field as shown in the official docs.

            // Save the PDF (now containing an empty signature field)
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"PDF processed successfully and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
