using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchPdfSigner
{
    // Simple mapping from a document title keyword to a certificate file.
    private static readonly Dictionary<string, (string CertPath, string Password)> CertificateMap =
        new Dictionary<string, (string, string)>(StringComparer.OrdinalIgnoreCase)
    {
        { "Finance", ("certs/finance.pfx", "finPass") },
        { "Legal",   ("certs/legal.pfx",   "legPass") },
        // default fallback
        { "Default", ("certs/default.pfx", "defPass") }
    };

    static void Main()
    {
        const string inputFolder  = @"C:\PdfBatch\Input";
        const string outputFolder = @"C:\PdfBatch\Signed";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document.
                using (Document doc = new Document(pdfPath))
                {
                    // Determine which certificate to use based on the document title.
                    string title = doc.Info.Title ?? string.Empty;
                    var certInfo = SelectCertificate(title);

                    // Create a concrete PKCS7 signature object from the selected certificate.
                    PKCS7 signature = new PKCS7(certInfo.CertPath, certInfo.Password);

                    // OPTIONAL: set signature appearance properties.
                    signature.Reason   = "Batch signing";
                    signature.Location = "Company HQ";

                    // Locate the signature field. Adjust the field name as needed.
                    const string signatureFieldName = "Signature1";
                    SignatureField sigField = doc.Form[signatureFieldName] as SignatureField;

                    if (sigField == null)
                    {
                        // If the field does not exist, create one on the first page.
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                        sigField = new SignatureField(doc, rect);
                        sigField.PartialName = signatureFieldName;
                        doc.Form.Add(sigField);
                    }

                    // Sign the document using the signature field.
                    sigField.Sign(signature);

                    // Save the signed PDF.
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                    doc.Save(outputPath);
                    Console.WriteLine($"Signed: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }

    // Selects a certificate based on a keyword found in the document title.
    private static (string CertPath, string Password) SelectCertificate(string title)
    {
        foreach (var kvp in CertificateMap)
        {
            if (kvp.Key != "Default" && title.IndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                return kvp.Value;
        }
        // Fallback to default certificate.
        return CertificateMap["Default"];
    }
}
