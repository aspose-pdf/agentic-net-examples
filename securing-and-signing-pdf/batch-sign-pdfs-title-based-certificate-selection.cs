using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchSigner
{
    // Directory containing PDFs to process
    private const string InputFolder = @"C:\PdfBatch\Input";
    // Directory where signed PDFs will be saved
    private const string OutputFolder = @"C:\PdfBatch\Signed";
    // Directory containing certificate files (e.g., Title.pfx)
    private const string CertFolder = @"C:\PdfBatch\Certificates";
    // Password for all PFX files (adjust as needed)
    private const string CertPassword = "pfxPassword";

    static void Main()
    {
        // Ensure output directory exists
        Directory.CreateDirectory(OutputFolder);

        // Gather all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(InputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string pdfPath in pdfFiles)
        {
            // Load the PDF document
            Document doc = new Document(pdfPath);

            // Retrieve a piece of metadata to decide which certificate to use.
            // Here we use the document title; fallback to file name without extension.
            string title = doc.Info.Title;
            if (string.IsNullOrWhiteSpace(title))
            {
                title = Path.GetFileNameWithoutExtension(pdfPath);
            }

            // Build the expected certificate file path (e.g., "Certificates\MyReport.pfx")
            string certPath = Path.Combine(CertFolder, $"{title}.pfx");

            // Verify that the certificate file exists; if not, skip this document.
            if (!File.Exists(certPath))
            {
                Console.WriteLine($"Certificate not found for '{title}'. Skipping '{pdfPath}'.");
                continue;
            }

            // ------------------------------------------------------------
            // Create a visible signature field on the first page.
            // ------------------------------------------------------------
            Page firstPage = doc.Pages[1];
            var sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150); // left, bottom, right, top (points)
            var signatureField = new SignatureField(firstPage, sigRect)
            {
                PartialName = "Signature"
            };
            // Add the field to the document's form collection.
            doc.Form.Add(signatureField, firstPage.Number);

            // ------------------------------------------------------------
            // Prepare the PKCS#7 signature using the certificate.
            // ------------------------------------------------------------
            var pkcs7 = new PKCS7(certPath, CertPassword)
            {
                Reason = "Document approval",
                Location = "Head Office",
                ContactInfo = "support@example.com",
                // Optional custom visual appearance for the signature.
                CustomAppearance = new SignatureCustomAppearance
                {
                    FontSize = 8,
                    ShowReason = true,
                    ShowLocation = true,
                    ShowContactInfo = true,
                    DigitalSignedLabel = "Signed by"
                }
            };

            // Sign the field with the prepared PKCS#7 object.
            signatureField.Sign(pkcs7);

            // Determine output file path and save the signed PDF.
            string outputPath = Path.Combine(OutputFolder, Path.GetFileName(pdfPath));
            doc.Save(outputPath);
            Console.WriteLine($"Signed '{pdfPath}' -> '{outputPath}'.");
        }

        Console.WriteLine("Batch signing completed.");
    }
}
