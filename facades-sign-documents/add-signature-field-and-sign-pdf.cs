using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, certificate file and password
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

        // Signature field parameters
        const string fieldName  = "Signature1";
        const int    pageNumber = 1;               // 1‑based page index
        const float  llx        = 100f;            // lower‑left X
        const float  lly        = 500f;            // lower‑left Y
        const float  urx        = 300f;            // upper‑right X
        const float  ury        = 600f;            // upper‑right Y

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
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Add a signature field to the specified page
                using (FormEditor formEditor = new FormEditor(doc))
                {
                    // FieldType.Signature creates a signature form field
                    bool added = formEditor.AddField(FieldType.Signature, fieldName,
                                                    pageNumber, llx, lly, urx, ury);
                    if (!added)
                    {
                        Console.Error.WriteLine("Failed to add signature field.");
                        return;
                    }
                    // No explicit Save() call is required – changes are kept in the Document instance.
                }

                // Prepare the signature object (PKCS#7 in this example)
                PKCS7 signature = new PKCS7(certPath, certPass);
                signature.Reason       = "Document approval";
                signature.Location     = "New York";
                signature.ContactInfo  = "john.doe@example.com";

                // Sign the document using the previously created field
                using (PdfFileSignature pdfSigner = new PdfFileSignature(doc))
                {
                    pdfSigner.SetCertificate(certPath, certPass);
                    // Optional: set an image to appear as the visual signature
                    // pdfSigner.SignatureAppearance = "signature_image.png";

                    // Sign using the field name; the field must be empty
                    pdfSigner.Sign(fieldName, signature);
                    pdfSigner.Save(outputPdf);
                }
            }

            Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
