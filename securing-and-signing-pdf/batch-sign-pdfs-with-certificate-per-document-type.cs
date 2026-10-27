using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchSigner
{
    static void Main()
    {
        // Folder where signed PDFs will be written
        string outputFolder = "SignedPdfs";
        Directory.CreateDirectory(outputFolder);

        // Get the list of PDF files that need to be signed (sample in‑memory data)
        List<string> pdfFiles = GetPdfFilePaths();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Read the document type metadata from the PDF (custom key "DocType")
            string docType;
            using (Document doc = new Document(pdfPath))
            {
                docType = doc.Info["DocType"] ?? string.Empty;
            }

            // Retrieve the appropriate certificate for this document type (sample stub)
            X509Certificate2 cert = GetCertificateForDocType(docType);
            if (cert == null)
            {
                Console.Error.WriteLine($"No certificate found for document type '{docType}'. Skipping {pdfPath}.");
                continue;
            }

            // Build the output file name
            string signedPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(pdfPath) + "_signed.pdf");

            // Sign the PDF using core Aspose.Pdf APIs (SignatureField + PKCS7)
            Document signedDoc = new Document(pdfPath);

            // Create a signature field on the first page
            var signatureRect = new Aspose.Pdf.Rectangle(100, 100, 200, 150);
            var signatureField = new SignatureField(signedDoc.Pages[1], signatureRect)
            {
                PartialName = "Signature"
            };
            signedDoc.Form.Add(signatureField, 1);

            // Prepare PKCS7 object from the X509Certificate2 instance
            // Aspose.Pdf.Forms.PKCS7 in this version expects a file path, so we write the cert to a temporary PFX file.
            string tempPfxPath = Path.GetTempFileName();
            try
            {
                byte[] certBytes = cert.Export(X509ContentType.Pfx);
                File.WriteAllBytes(tempPfxPath, certBytes);

                var pkcs7 = new PKCS7(tempPfxPath, string.Empty)
                {
                    Reason = $"Signed for document type {docType}",
                    Location = "Head Office",
                    ContactInfo = "Support"
                };

                // Optional custom visual appearance
                pkcs7.CustomAppearance = new SignatureCustomAppearance
                {
                    ShowContactInfo = true,
                    ShowLocation = true,
                    ContactInfoLabel = "Contact:",
                    LocationLabel = "Location:"
                };

                // Attach the PKCS7 signature to the field using the Sign method
                signatureField.Sign(pkcs7);
            }
            finally
            {
                // Clean up the temporary file
                if (File.Exists(tempPfxPath))
                {
                    try { File.Delete(tempPfxPath); } catch { /* ignore cleanup errors */ }
                }
            }

            // Save the signed PDF
            signedDoc.Save(signedPath);
            Console.WriteLine($"Signed PDF saved to '{signedPath}'.");
        }
    }

    // Retrieves a sample list of PDF file paths (in‑memory placeholder)
    static List<string> GetPdfFilePaths()
    {
        var list = new List<string>();
        // In a real scenario this would come from a database. Here we use a stub.
        // Add sample paths (adjust as needed for testing).
        list.Add("sample1.pdf");
        list.Add("sample2.pdf");
        return list;
    }

    // Retrieves a stub X509Certificate2 for a given document type.
    // In a real implementation this would query a database or certificate store.
    static X509Certificate2 GetCertificateForDocType(string docType)
    {
        // For demonstration we return a newly generated self‑signed certificate if needed.
        // Here we simply return an empty certificate object to keep the example compile‑ready.
        try
        {
            // If you have a PFX file you could load it like:
            // return new X509Certificate2("path_to_cert.pfx", "password");
            return new X509Certificate2();
        }
        catch
        {
            return null;
        }
    }
}
