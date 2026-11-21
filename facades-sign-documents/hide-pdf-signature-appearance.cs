using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_hidden_appearance.pdf";
        const string certPath       = "certificate.pfx";
        const string certPassword   = "certPassword";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate not found: {certPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Create the signature facade and bind the loaded document
                using (PdfFileSignature signer = new PdfFileSignature())
                {
                    signer.BindPdf(pdfDocument);

                    // Create PKCS7 signature object – this is the class Aspose.Pdf expects
                    PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
                    {
                        // Optional: clear textual fields to keep the appearance empty
                        Reason      = string.Empty,
                        Location    = string.Empty,
                        ContactInfo = string.Empty
                    };

                    // Sign with an invisible (hidden) appearance. Using the overload that
                    // takes a signature name and a PKCS7 object creates a signature field
                    // that is not rendered on the page.
                    signer.Sign("Signature1", pkcs7);
                }

                // Save the signed PDF; no special SaveOptions needed for PDF output
                pdfDocument.Save(outputPdfPath);
            }

            Console.WriteLine($"Signed PDF saved to '{outputPdfPath}' with hidden signature appearance.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
