using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class BatchSigner
{
    static void Main()
    {
        // Input folder containing PDFs to be signed
        const string inputFolder = @"C:\PdfInput";
        // Output folder for signed PDFs
        const string outputFolder = @"C:\PdfSigned";

        // Path to the PFX certificate and its password
        const string certPath = @"C:\certificates\mycert.pfx";
        const string certPassword = "certPassword";

        // Timestamp authority settings
        const string tsaUrl = "https://timestamp.example.com";
        // Optional: basic auth credentials in "user:password" format
        const string tsaAuth = null; // e.g., "user:pass" or null if not required

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input directory
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document (using block ensures proper disposal)
                using (Document doc = new Document(pdfFile))
                {
                    // Create a signature field on the first page.
                    // Position: lower‑left (100, 100), width 200, height 50.
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
                    SignatureField sigField = new SignatureField(doc.Pages[1], rect)
                    {
                        // Optional visual properties
                        Color = Aspose.Pdf.Color.LightGray,
                        // Name of the field (must be unique within the document)
                        Name = "Signature1"
                    };
                    // Add the signature field to the page's annotations collection
                    doc.Pages[1].Annotations.Add(sigField);

                    // Create a PKCS#7 signature object using the certificate file
                    PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
                    {
                        // Basic signature metadata
                        Reason = "Document approved",
                        Location = "Company HQ",
                        ContactInfo = "security@example.com",
                        // Show default signature appearance (subject, date, etc.)
                        ShowProperties = true
                    };

                    // Configure timestamp settings (optional but requested)
                    TimestampSettings tsa = new TimestampSettings(tsaUrl, tsaAuth, DigestHashAlgorithm.Sha256);
                    pkcs7.TimestampSettings = tsa;

                    // Sign the document using the signature field
                    sigField.Sign(pkcs7);

                    // Build output file path (preserve original file name)
                    string outputPath = System.IO.Path.Combine(outputFolder, System.IO.Path.GetFileName(pdfFile));

                    // Save the signed PDF (Document.Save(string) writes PDF regardless of extension)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Signed: {System.IO.Path.GetFileName(pdfFile)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error signing '{System.IO.Path.GetFileName(pdfFile)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch signing completed.");
    }
}
