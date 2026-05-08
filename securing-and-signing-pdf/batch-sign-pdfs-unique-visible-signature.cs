using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchPdfSigner
{
    static void Main()
    {
        // Input PDF files and corresponding signature images (must have same length)
        string[] pdfFiles   = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string[] imageFiles = { "sign1.png", "sign2.png", "sign3.png" };

        // Certificate (PFX) used for all signatures
        const string certPath = "certificate.pfx";
        const string certPassword = "pfxPassword";

        // Reason, contact and location (can be customized per document if needed)
        const string reason  = "Approved";
        const string contact = "signer@example.com";
        const string location = "Head Office";

        for (int i = 0; i < pdfFiles.Length; i++)
        {
            string pdfPath   = pdfFiles[i];
            string imagePath = imageFiles[i];
            string outputPath = Path.Combine(
                Path.GetDirectoryName(pdfPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(pdfPath) + "_signed.pdf");

            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
                continue;
            }
            if (!File.Exists(imagePath))
            {
                Console.Error.WriteLine($"Signature image not found: {imagePath}");
                continue;
            }

            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Choose the page where the signature will be placed (first page in this example)
                    Page page = doc.Pages[1];

                    // Define the rectangle for the signature field (coordinates are in points)
                    // Adjust the values as needed for your layout
                    Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

                    // Add a visible signature image at the same location
                    // This image will appear on the page regardless of the digital signature
                    page.AddImage(imagePath, sigRect);

                    // Create a signature field and add it to the form
                    SignatureField sigField = new SignatureField(doc, sigRect)
                    {
                        PartialName = $"Signature_{i + 1}"
                    };
                    doc.Form.Add(sigField);

                    // Prepare the PKCS#7 signature object
                    PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
                    {
                        Reason      = reason,
                        ContactInfo = contact,
                        Location    = location
                    };

                    // Sign the field with the prepared signature
                    sigField.Sign(pkcs7);

                    // Save the signed PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Signed PDF saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}