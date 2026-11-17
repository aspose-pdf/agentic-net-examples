using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing; // for Color, Border, etc.
using IOPath = System.IO.Path; // alias to disambiguate System.IO.Path from Aspose.Pdf.Drawing.Path

class BatchPdfSigner
{
    static void Main()
    {
        // Input directory containing PDFs to be signed
        const string inputDir = @"C:\PdfInput";
        // Output directory where signed PDFs will be saved
        const string outputDir = @"C:\PdfSigned";

        // Path to the certificate (PFX) and its password
        const string certPath = @"C:\certificates\mycert.pfx";
        const string certPassword = "certPassword";

        // Timestamp Authority (TSA) URL (optional username/password if required)
        const string tsaUrl = "https://tsa.example.com";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Iterate over all PDF files in the input directory
        foreach (string pdfFilePath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            // Use the alias to avoid the ambiguous Path reference
            string outputFilePath = IOPath.Combine(outputDir, IOPath.GetFileName(pdfFilePath));

            // Load the PDF into an expandable memory stream (required for core signing API)
            byte[] pdfBytes = File.ReadAllBytes(pdfFilePath);
            using (var expandableStream = new MemoryStream())
            {
                // Copy bytes into a stream that can grow (MemoryStream without a fixed buffer)
                expandableStream.Write(pdfBytes, 0, pdfBytes.Length);
                expandableStream.Position = 0;

                // Load document from the expandable stream
                using (var doc = new Document(expandableStream))
                {
                    // Define the rectangle where the visible signature will appear
                    var sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

                    // Create a signature field on the first page (Pages collection is 1‑based)
                    var signatureField = new SignatureField(doc.Pages[1], sigRect)
                    {
                        PartialName = "Signature1"
                    };
                    doc.Form.Add(signatureField, 1);

                    // Prepare PKCS#7 signature object with certificate and appearance settings
                    var pkcs7 = new PKCS7(certPath, certPassword)
                    {
                        Reason = "Document approved",
                        Location = "Company HQ",
                        ContactInfo = "contact@example.com",
                        CustomAppearance = new SignatureCustomAppearance
                        {
                            FontSize = 8,
                            ShowContactInfo = true
                        },
                        TimestampSettings = new TimestampSettings(tsaUrl, string.Empty, DigestHashAlgorithm.Sha256)
                    };

                    // Sign the document – this writes the signed PDF back into the same stream
                    signatureField.Sign(pkcs7);
                }

                // After signing, the stream now contains the signed PDF bytes
                expandableStream.Position = 0;
                File.WriteAllBytes(outputFilePath, expandableStream.ToArray());
            }

            Console.WriteLine($"Signed PDF saved to: {outputFilePath}");
        }

        Console.WriteLine("Batch signing completed.");
    }
}
