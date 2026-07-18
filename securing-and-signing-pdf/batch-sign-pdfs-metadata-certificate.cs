using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchPdfSigner
{
    // Map document title (metadata) to certificate file and password.
    // Return null if no mapping exists for the given title.
    private static (string certPath, string password)? GetCertificateInfo(string title)
    {
        // Example mappings – adjust as needed.
        switch (title)
        {
            case "Contract":
                return ("certs/contract.pfx", "contractPass");
            case "Invoice":
                return ("certs/invoice.pfx", "invoicePass");
            default:
                return null;
        }
    }

    // Sign a single PDF using the certificate selected by its metadata.
    private static void SignPdf(string inputPath, string outputDirectory)
    {
        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Retrieve metadata (Title) to decide which certificate to use.
            string title = doc.Info.Title ?? string.Empty;
            var certInfo = GetCertificateInfo(title);

            if (certInfo == null)
            {
                Console.WriteLine($"No certificate mapping for title \"{title}\" – skipping {Path.GetFileName(inputPath)}.");
                return;
            }

            // Create a signature field on the first page.
            // Fully qualify Rectangle to avoid ambiguity.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);
            SignatureField signatureField = new SignatureField(doc, rect);

            // Add the signature field to the page's annotations collection.
            Page page = doc.Pages[1]; // 1‑based indexing as per Aspose.Pdf rules.
            page.Annotations.Add(signatureField);

            // Create the signature object using the selected certificate.
            // PKCS7 is a common choice for PDF signatures.
            Signature signature = new PKCS7(certInfo.Value.certPath, certInfo.Value.password);

            // Sign the document via the signature field.
            signatureField.Sign(signature);

            // Ensure the output directory exists.
            Directory.CreateDirectory(outputDirectory);
            string outputPath = Path.Combine(outputDirectory, Path.GetFileName(inputPath));
            doc.Save(outputPath);
            Console.WriteLine($"Signed PDF saved to {outputPath}");
        }
    }

    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: BatchPdfSigner <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (var pdfPath in pdfFiles)
        {
            try
            {
                SignPdf(pdfPath, outputFolder);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {Path.GetFileName(pdfPath)}: {ex.Message}");
            }
        }
    }
}
