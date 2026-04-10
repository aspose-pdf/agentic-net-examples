using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchSigner
{
    static void Main()
    {
        // Input and output directories
        const string inputDir = "InputPdfs";
        const string outputDir = "SignedPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify that the input directory exists; if not, create it and exit gracefully.
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory '{inputDir}' does not exist. Creating it now. Place PDFs to be signed in this folder and re‑run the program.");
            Directory.CreateDirectory(inputDir);
            return; // Nothing to process yet.
        }

        // Mapping from document title (metadata) to certificate file and password
        var certificateMap = new Dictionary<string, (string CertPath, string Password)>(StringComparer.OrdinalIgnoreCase)
        {
            { "CompanyA", ("certs/companyA.pfx", "passA") },
            { "CompanyB", ("certs/companyB.pfx", "passB") }
            // Add more mappings as needed
        };

        // Default certificate if metadata does not match any entry
        var defaultCertificate = ("certs/default.pfx", "defaultPass");

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            try
            {
                // Load the PDF document (using rule: wrap Document in using)
                using (Document doc = new Document(pdfPath))
                {
                    // Retrieve a piece of metadata to decide which certificate to use.
                    // Here we use the document Title; fallback to empty string if not set.
                    string title = doc.Info.Title ?? string.Empty;

                    // Select the appropriate certificate based on the title.
                    (string certPath, string certPassword) certInfo;
                    if (!certificateMap.TryGetValue(title, out certInfo))
                    {
                        certInfo = defaultCertificate;
                    }

                    // Ensure the certificate file exists.
                    if (!File.Exists(certInfo.certPath))
                    {
                        Console.Error.WriteLine($"Certificate not found: {certInfo.certPath}. Skipping file {Path.GetFileName(pdfPath)}.");
                        continue;
                    }

                    // Create (or locate) a signature field.
                    // For simplicity we add a new signature field on the first page.
                    Page firstPage = doc.Pages[1];
                    // Fully qualified rectangle to avoid ambiguity.
                    Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                    SignatureField signatureField = new SignatureField(doc, sigRect);
                    firstPage.Annotations.Add(signatureField);

                    // Load the certificate into a concrete PKCS7 signature object.
                    // PKCS7 derives from the abstract Signature class and can be instantiated.
                    PKCS7 pkcs7 = new PKCS7(certInfo.certPath, certInfo.certPassword);
                    pkcs7.Reason = "Document approved";
                    pkcs7.Location = "Head Office";

                    // Sign the document using the signature field.
                    signatureField.Sign(pkcs7);

                    // Save the signed PDF to the output directory.
                    string outputPath = Path.Combine(outputDir, Path.GetFileName(pdfPath));
                    doc.Save(outputPath);
                    Console.WriteLine($"Signed PDF saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        }
    }
}
