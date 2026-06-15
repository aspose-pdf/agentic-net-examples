using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchPdfSigner
{
    // Simple holder for certificate information
    private class CertInfo
    {
        public string Path { get; set; }
        public string Password { get; set; }

        public CertInfo(string path, string password)
        {
            Path = path;
            Password = password;
        }
    }

    // Mapping from a metadata keyword to the certificate that should be used
    private static readonly Dictionary<string, CertInfo> CertificateMap = new Dictionary<string, CertInfo>(StringComparer.OrdinalIgnoreCase)
    {
        // Example: if the PDF title contains "Contract", use ContractCert.pfx
        { "Contract", new CertInfo("certs/ContractCert.pfx", "c0ntr@ctPwd") },
        // Example: if the PDF author contains "Finance", use FinanceCert.pfx
        { "Finance", new CertInfo("certs/FinanceCert.pfx", "fin@nc3Pwd") }
        // Add more mappings as required
    };

    static void Main()
    {
        const string inputFolder = "InputPdfs";
        const string outputFolder = "SignedPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    // Retrieve metadata to decide which certificate to use.
                    string title = doc.Info.Title ?? string.Empty;
                    string author = doc.Info.Author ?? string.Empty;

                    // Find the first matching rule based on title or author.
                    CertInfo selectedCert = null;
                    foreach (var kvp in CertificateMap)
                    {
                        if (title.IndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            author.IndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            selectedCert = kvp.Value;
                            break;
                        }
                    }

                    if (selectedCert == null)
                    {
                        Console.WriteLine($"No matching certificate for '{Path.GetFileName(pdfPath)}'; skipping.");
                        continue;
                    }

                    // Prepare PKCS7 signature object using the selected certificate.
                    PKCS7 pkcs7 = new PKCS7(selectedCert.Path, selectedCert.Password)
                    {
                        Reason = "Document signed",
                        Location = "Automated Batch",
                        ContactInfo = "no-reply@example.com"
                    };

                    // Sign every signature field present in the document.
                    if (doc.Form != null && doc.Form.Fields != null)
                    {
                        foreach (Field field in doc.Form.Fields)
                        {
                            if (field is SignatureField sigField)
                            {
                                sigField.Sign(pkcs7);
                            }
                        }
                    }

                    // Save the signed document to the output folder.
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                    doc.Save(outputPath);
                    Console.WriteLine($"Signed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
