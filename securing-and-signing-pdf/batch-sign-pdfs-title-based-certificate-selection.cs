using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Map document titles (or any metadata) to certificate files.
    static readonly Dictionary<string, string> CertificateMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "Invoice", @"C:\Certificates\invoice_cert.pfx" },
        { "Report",  @"C:\Certificates\report_cert.pfx" },
        // default fallback
        { "Default", @"C:\Certificates\default_cert.pfx" }
    };

    // Password for all certificates (adjust as needed).
    const string CertificatePassword = "certPassword";

    static void Main()
    {
        const string inputFolder  = @"C:\PdfBatch\Input";
        const string outputFolder = @"C:\PdfBatch\Signed";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Open the PDF document.
                using (Document doc = new Document(pdfPath))
                {
                    // Determine which certificate to use based on document metadata.
                    string certPath = ResolveCertificatePath(doc);

                    if (!File.Exists(certPath))
                    {
                        Console.Error.WriteLine($"Certificate not found for '{pdfPath}'. Expected at: {certPath}");
                        continue;
                    }

                    // Load the X.509 certificate without using the obsolete constructor.
                    X509Certificate2 cert = new X509Certificate2();
                    cert.Import(File.ReadAllBytes(certPath), CertificatePassword, X509KeyStorageFlags.DefaultKeySet);

                    // Create a signature field on the first page.
                    // Position rectangle: lower‑left (100,100), upper‑right (200,150)
                    var rect = new Aspose.Pdf.Rectangle(100, 100, 200, 150);
                    var signatureField = new SignatureField(doc.Pages[1], rect)
                    {
                        PartialName = "Signature1"
                    };

                    // Add the signature field to the document's form.
                    doc.Form.Add(signatureField, 1);

                    // -----------------------------------------------------------------
                    // OPTIONAL: If the Aspose.Pdf.Signatures assembly is referenced, the
                    // following block can be uncommented to apply a real PKCS#7 signature.
                    // -----------------------------------------------------------------
                    // using Aspose.Pdf.Signatures;
                    // var pkcsSignature = new PkcsSignature(cert)
                    // {
                    //     ContactInfo = "contact@example.com",
                    //     Location    = "Head Office",
                    //     Reason      = "Document approval"
                    // };
                    // signatureField.Signature = pkcsSignature;

                    // Save the (potentially) signed PDF.
                    string signedPath = Path.Combine(outputFolder,
                        Path.GetFileNameWithoutExtension(pdfPath) + "_signed.pdf");
                    doc.Save(signedPath);

                    Console.WriteLine($"Signed PDF saved: {signedPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }

    // Resolve the certificate file path based on document metadata.
    static string ResolveCertificatePath(Document doc)
    {
        // Example: use the document title to pick a certificate.
        // If the title is not set or not found in the map, use the default certificate.
        string title = doc.Info.Title ?? string.Empty;

        if (CertificateMap.TryGetValue(title, out string certPath))
        {
            return certPath;
        }

        // Fallback to default certificate.
        return CertificateMap["Default"];
    }
}
